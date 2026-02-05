import api from "./api";

export const getDefaultConfig = (modelId, qty) =>
  api.get(`/default-config/${modelId}`, {
    params: { qty }
  }).then(r => r.data);
