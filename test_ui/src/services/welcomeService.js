import api from "./api";

export const getSegments = () =>
  api.get("/welcome/segments").then(r => r.data);

export const getManufacturers = (segId) =>
  api.get(`/welcome/manufacturers/${segId}`).then(r => r.data);

export const getModels = (segId, mfgId) =>
  api.get(`/welcome/models`, {
    params: { segId, mfgId }
  }).then(r => r.data);
