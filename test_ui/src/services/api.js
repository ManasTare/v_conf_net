import axios from "axios";

const api = axios.create({
  baseURL: "/api", // proxy handles backend
  headers: {
    "Content-Type": "application/json"
  }
});

export default api;
