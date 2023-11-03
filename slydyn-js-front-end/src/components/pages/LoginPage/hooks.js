// @ts-check
import { useFormik } from "formik";
import authServiceInstance from "@/services/Auth.service";
import { useAbortControllerWithMaker } from "@/hooks/useAbortController";

/**
 *
 * @param {Object} params
 * @param {() => void} params.onLoginSuccess
 * @param {(err: Error) => void} params.onLoginError
 */
function useLoginForm({
  onLoginSuccess = () => {},
  onLoginError = (err) => {},
}) {
  const [abortController, makeAbortController] = useAbortControllerWithMaker();

  const signupForm = useFormik({
    initialValues: {
      username: "",
      password: "",
    },
    onSubmit: (values, helpers) => {
      handleSubmit(values, helpers)
        .then(() => onLoginSuccess())
        .catch((err) => onLoginError(err));
    },
    validate: (values) => {
      const errors = {};
      // all fields are required
      if (!values.username) {
        errors.username = "Username is required";
      }

      if (!values.password) {
        errors.password = "Password is required";
      }

      return errors;
    },
    validateOnChange: true,
    validateOnBlur: true,
    validateOnMount: false,
  });

  /**
   *
   * @param {typeof signupForm.values} values
   * @param {import('formik').FormikHelpers} formikHelpers
   */
  async function handleSubmit(values, formikHelpers) {
    formikHelpers.setSubmitting(true);
    if (abortController.signal.aborted) {
      makeAbortController();
    }
    try {
      await authServiceInstance.loginWithSession(
        values,
        abortController.signal
      );
      formikHelpers.setSubmitting(false);
    } catch (error) {
      formikHelpers.setSubmitting(false);
      if (error.name === "CanceledError") {
        console.log("Login request was canceled");
        return;
      }
      throw error;
    }
  }

  return {
    signupForm,
  };
}

export { useLoginForm };
