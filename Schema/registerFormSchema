import * as Yup from "yup";

const registerFormSchema = Yup.object().shape({
  email: Yup.string()
    .email("*Invalid email format")
    .max(40, "*Cannot exceed 40 characters")
    .required("*Required field"),
  password: Yup.string()
    .required("*Please enter your password")
    .matches(
      /^.*(?=.{8,})((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$/,
      "Password must contain at least 8 characters, one uppercase, one number and one special case character"
    ),
  confirmPassword: Yup.string().required("*Please enter your password"),
});

export default registerFormSchema;
