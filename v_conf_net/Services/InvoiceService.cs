using Microsoft.EntityFrameworkCore;
using v_conf_net.DTOs;
using v_conf_net.Models;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Services;

public class InvoiceService : IInvoiceService
{
    private readonly AppDbContext _context;

    public InvoiceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateInvoiceAsync(InvoiceRequestDto request)
    {
        // 1. Fetch User & Model
        var user = await _context.Users.FindAsync(request.UserId);
        if (user == null)
            throw new Exception("User not found");

        var model = await _context.Models.FindAsync(request.ModelId);
        if (model == null)
            throw new Exception("Model not found");

        // 2. Base Amount
        double baseAmt = model.Price;
        double baseTotal = baseAmt * request.Qty;
        double deltaSum = 0; // Future: Calculate from Alternate Components

        double amount = baseTotal + deltaSum;
        double tax = amount * 0.18;
        double totalAmt = amount + tax;

        // 3. Create Invoice Header
        var invoice = new InvoiceHeader
        {
            UserId = user.Id,
            ModelId = model.ModelId,
            Qty = request.Qty,
            BaseAmt = baseTotal,
            Tax = tax,
            TotalAmt = totalAmt,
            InvDate = DateOnly.FromDateTime(DateTime.Now),
            Status = "Confirmed",
            CustomerDetail = request.CustomerDetail ?? ""
        };

        _context.InvoiceHeaders.Add(invoice);
        await _context.SaveChangesAsync(); // Save to generate InvId

        // 4. Create Invoice Details
        // Logic: Since we skipped "Modify Config", we assume standard defaults.
        // 4. Resolve Final Configuration
        var finalDetails = new List<InvoiceDetail>();
        
        // A. Standard Features (Non-Configurable)
        var standardDetails = await _context.VehicleDetails
            .Where(vd => vd.ModelId == model.ModelId && vd.IsConfig == "N")
            .Include(vd => vd.Comp)
            .ToListAsync();
            
        Console.WriteLine($"DEBUG: Found {standardDetails.Count} Standard Items (IsConfig='N')");
            
        foreach(var sd in standardDetails)
        {
             finalDetails.Add(new InvoiceDetail{
                 InvId = invoice.InvId,
                 CompId = sd.CompId ?? 0,
                 CompPrice = sd.Comp?.Price ?? 0
             });
        }

        // B. Configurable Components (Defaults + Alternates)
        var defaultConfigs = await _context.VehicleDefaultConfigs
            .Where(vdc => vdc.ModelId == model.ModelId)
            .Include(vdc => vdc.Comp)
            .ToListAsync();

        var alternates = await _context.AlternateComponentMasters
            .Where(acm => acm.ModelId == model.ModelId)
            .Include(acm => acm.AltComp)
            .ToListAsync();



        Console.WriteLine($"DEBUG: Found {defaultConfigs.Count} Default Configs");
        Console.WriteLine($"DEBUG: Found {alternates.Count} Alternate Selections");

        deltaSum = 0;

        foreach (var dc in defaultConfigs)
        {
            // Check if this default component has been replaced
            // Match by Component Type? Or by the fact that the Original CompId matches?
            // VehicleDefaultConfig tells us the "Default Option" for a slot.
            // AlternateComponentMaster tells us "Original CompId -> Alt CompId".
            // So if ACM.CompId == DC.CompId, it means the user replaced THIS default.
            
            var replacement = alternates.FirstOrDefault(a => a.CompId == dc.CompId);

            if (replacement != null)
            {
                // Use Alternate
                finalDetails.Add(new InvoiceDetail
                {
                    InvId = invoice.InvId,
                    CompId = replacement.AltCompId,
                    CompPrice = replacement.AltComp.Price.GetValueOrDefault()
                });
                deltaSum += replacement.DeltaPrice;
            }
            else
            {
                // Keep Default
                finalDetails.Add(new InvoiceDetail
                {
                    InvId = invoice.InvId,
                    CompId = dc.CompId,
                    CompPrice = dc.Comp.Price.GetValueOrDefault()
                });
            }
        }
        
        // 5. Update Totals
        baseTotal = baseAmt * request.Qty;
        double finalTotal = baseTotal + deltaSum;
        double finalTax = finalTotal * 0.18;
        double finalGrandTotal = finalTotal + finalTax;

        invoice.BaseAmt = baseTotal;
        invoice.TotalAmt = finalGrandTotal;
        invoice.Tax = finalTax;

        _context.InvoiceHeaders.Update(invoice);
        _context.InvoiceDetails.AddRange(finalDetails);

        // Logic moved above

        await _context.SaveChangesAsync();

        return $"Invoice {invoice.InvId} Generated Successfully! Total: {invoice.TotalAmt:C}";
    }
}
