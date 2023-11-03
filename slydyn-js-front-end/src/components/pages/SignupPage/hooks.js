// @ts-check
import { useFormik } from "formik";
import authServiceInstance from "@/services/Auth.service";
import useAbortController, {
  useAbortControllerWithMaker,
} from "@/hooks/useAbortController";

/**
 *
 * @param {Object} params
 * @param {() => void} params.onSignupSuccess
 * @param {(err: Error) => void} params.onSignupError
 */
function useSignupForm({
  onSignupSuccess = () => {},
  onSignupError = (err) => {},
}) {
  const [abortController, makeAbortController] = useAbortControllerWithMaker();

  const signupForm = useFormik({
    initialValues: {
      username: "",
      email: "",
      password: "",
      role: "",
    },
    onSubmit: (values, helpers) => {
      handleSubmit(values, helpers)
        .then(() => onSignupSuccess())
        .catch((err) => onSignupError(err));
    },
    validate: (values) => {
      const errors = {};
      // all fields are required
      if (!values.username) {
        errors.username = "Username is required";
      }

      if (!values.email) {
        errors.email = "Email is required";
      }

      if (!values.password) {
        errors.password = "Password is required";
      }

      if (!values.role) {
        errors.role = "Role is required";
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
      await authServiceInstance.signup(values, abortController.signal);
      formikHelpers.setSubmitting(false);
    } catch (error) {
      formikHelpers.setSubmitting(false);
      if (error.name === "CanceledError") {
        console.log("Signup request was canceled");
        return;
      }
      throw error;
    }
  }

  return {
    signupForm,
  };
}

export { useSignupForm };
