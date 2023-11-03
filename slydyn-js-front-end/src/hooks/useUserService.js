// @ts-check
import authServiceInstance from "@/services/Auth.service";
import useAbortController from "./useAbortController";

export default function useUserService() {
  const abortController = useAbortController();
  /**
   *
   * @param {string} accessToken
   */
  async function fetchMe(accessToken) {
    return await authServiceInstance.fetchMe(
      accessToken,
      abortController.signal
    );
  }

  return {
    fetchMe,
  };
}
