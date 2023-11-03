// @ts-check
import { useEffect, useRef } from "react";

export default function useAbortController() {
  const abortControllerRef = useRef(new AbortController());

  useEffect(() => {
    const refPointer = abortControllerRef;
    return () => {
      console.log("Unmount detected");
      if (refPointer.current.signal.aborted) return;
      refPointer.current.abort();
    };
  }, []);

  return abortControllerRef.current;
}

/**
 *
 * @returns {[AbortController, () => void]}
 */
export function useAbortControllerWithMaker() {
  const abortControllerRef = useRef(new AbortController());

  useEffect(() => {
    const refPointer = abortControllerRef;
    return () => {
      console.log("Unmount detected");
      if (refPointer.current.signal.aborted) return;
      refPointer.current.abort();
    };
  }, []);

  const makeAbortController = () => {
    abortControllerRef.current = new AbortController();
  };

  return [abortControllerRef.current, makeAbortController];
}
