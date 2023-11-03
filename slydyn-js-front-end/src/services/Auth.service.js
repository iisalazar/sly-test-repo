// @ts-check
import httpClient from "@/lib/httpClient";
import { mapToDtoSafe } from "@/lib/mapper";
import { UserSchema } from "../schemas/User/UserSchema";

/**
 * @typedef {Object} SignupRequestFields
 * @property {string} username
 * @property {string} email
 * @property {string} password
 * @property {string} role
 */

/**
 * @typedef {Object} LoginRequestFields
 * @property {string} username
 * @property {string} password
 */

class AuthService {
  /**
   *
   * @param {import('axios').AxiosInstance} httpClient
   */
  constructor(httpClient) {
    this.httpClient = httpClient;
  }

  /**
   *
   * @param {SignupRequestFields} values
   *
   * @param {AbortSignal} [signal]
   */
  async signup(values, signal = undefined) {
    const res = await this.httpClient.post("/api/auth/register", values, {
      signal,
      headers: {
        "Content-Type": "application/json",
      },
    });

    return res;
  }

  /**
   *
   * @param {LoginRequestFields} values
   * @param {AbortSignal} [signal]
   * @returns {Promise<import('axios').AxiosResponse<{ accessToken: string, refreshToken: string }>>}
   */
  async login(values, signal = undefined) {
    const res = await this.httpClient.post("/api/auth/login", values, {
      signal,
      headers: {
        "Content-Type": "application/json",
      },
    });

    return res;
  }

  /**
   *
   * @param {string} accessToken
   * @param {AbortSignal} [signal]
   * @returns {Promise<import('../schemas/User/UserSchema').UserDto>}
   */
  async fetchMe(accessToken, signal) {
    try {
      const res = await this.httpClient.get("/api/auth/me", {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
        signal,
      });
      const parsedData = mapToDtoSafe(res.data, UserSchema);
      return parsedData;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}

const authServiceInstance = new AuthService(httpClient);

export default authServiceInstance;
