import { useEffect, useState } from "react";
import {
  getSegments,
  getManufacturers,
  getModels
} from "./services/welcomeService";
import { getDefaultConfig } from "./services/defaultConfigService";

export default function Welcome() {

  // ===============================
  // State
  // ===============================
  const [segments, setSegments] = useState([]);
  const [manufacturers, setManufacturers] = useState([]);
  const [models, setModels] = useState([]);

  const [segmentId, setSegmentId] = useState("");
  const [mfgId, setMfgId] = useState("");
  const [modelId, setModelId] = useState("");
  const [qty, setQty] = useState("");

  const [result, setResult] = useState(null);
  const [loading, setLoading] = useState(false);

  // ===============================
  // Load Segments (page load)
  // ===============================
  useEffect(() => {
    fetchSegments();
  }, []);

  const fetchSegments = async () => {
    try {
      const data = await getSegments();
      setSegments(data);
    } catch (err) {
      console.error(err);
    }
  };

  // ===============================
  // Segment Change
  // ===============================
  const handleSegmentChange = async (e) => {
    const id = e.target.value;

    setSegmentId(id);

    // reset downstream
    setMfgId("");
    setModelId("");
    setManufacturers([]);
    setModels([]);
    setResult(null);

    if (!id) return;

    const data = await getManufacturers(id);
    setManufacturers(data);
  };

  // ===============================
  // Manufacturer Change
  // ===============================
  const handleManufacturerChange = async (e) => {
    const id = e.target.value;

    setMfgId(id);

    // reset model
    setModelId("");
    setModels([]);
    setResult(null);

    if (!id) return;

    const data = await getModels(segmentId, id);
    setModels(data);
  };

  // ===============================
  // Submit (Default Config)
  // ===============================
  const handleSubmit = async () => {
    if (!modelId || !qty) return;

    setLoading(true);

    try {
      const res = await getDefaultConfig(modelId, qty); // GET call
      setResult(res);
    } catch (err) {
      console.error(err);
    }

    setLoading(false);
  };

  // ===============================
  // UI
  // ===============================
  return (
    <div style={styles.container}>

      <h2>Vehicle Selection</h2>

      {/* Segment */}
      <select value={segmentId} onChange={handleSegmentChange}>
        <option value="">Select Segment</option>
        {segments.map(s =>
          <option key={s.id} value={s.id}>{s.name}</option>
        )}
      </select>

      {/* Manufacturer */}
      <select
        value={mfgId}
        onChange={handleManufacturerChange}
        disabled={!segmentId}
      >
        <option value="">Select Manufacturer</option>
        {manufacturers.map(m =>
          <option key={m.id} value={m.id}>{m.name}</option>
        )}
      </select>

      {/* Model */}
      <select
        value={modelId}
        onChange={(e) => setModelId(e.target.value)}
        disabled={!mfgId}
      >
        <option value="">Select Model</option>
        {models.map(m =>
          <option key={m.id} value={m.id}>
            {m.name} (₹{m.price})
          </option>
        )}
      </select>

      {/* Quantity */}
      <input
        type="number"
        placeholder="Quantity"
        value={qty}
        onChange={(e) => setQty(e.target.value)}
        disabled={!modelId}
      />

      {/* Submit */}
      <button onClick={handleSubmit} disabled={!qty || loading}>
        {loading ? "Loading..." : "Get Default Configuration"}
      </button>

      {/* ===============================
          Result Section
         =============================== */}
      {result && (
        <div style={styles.result}>

          <h4>Configuration Summary</h4>

          <p><b>Model:</b> {result.modelName}</p>
          <p><b>Unit Price:</b> ₹{result.unitPrice}</p>
          <p><b>Quantity:</b> {result.quantity}</p>
          <p><b>Total Price:</b> ₹{result.totalPrice}</p>

          <hr />

          <h5>Default Components</h5>

          <ul>
            {result.components.map((c, i) => (
              <li key={i}>
                {c.name} (₹{c.price})
              </li>
            ))}
          </ul>

        </div>
      )}
    </div>
  );
}


// ===============================
// Styles
// ===============================
const styles = {
  container: {
    maxWidth: 420,
    margin: "60px auto",
    display: "flex",
    flexDirection: "column",
    gap: 12,
    padding: 20,
    border: "1px solid #ddd",
    borderRadius: 8
  },
  result: {
    marginTop: 20,
    padding: 12,
    border: "1px solid #ccc",
    borderRadius: 6,
    background: "#fafafa"
  }
};
