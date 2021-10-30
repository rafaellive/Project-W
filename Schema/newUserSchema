import * as yup from "yup";

const newUserSchema = yup.object().shape({
  email: yup
    .string()
    .email()
    .min(7, "7 characters minimum")
    .max(255, "255 characters maximum")
    .required(),
    password: yup
    .string()
    .required("Please enter your password")
    .matches(
      /^.*(?=.{8,})((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$/,
      "Password must contain at least 8 characters, one uppercase, one number and one special case character"
    ),
});

export default { newUserSchema };
