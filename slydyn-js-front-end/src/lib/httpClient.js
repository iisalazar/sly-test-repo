import axios from "axios";

const backendURI =
  process.env.NEXT_PUBLIC_BACKEND_URI || "https://localhost:7189/";

const httpClient = axios.create({
  baseURL: backendURI,
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

export default httpClient;
