import * as Yup from "yup";

const phoneRegExp = /^((\\([0-9]{3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/;

const providerFormSchema = Yup.object().shape({
  practice: Yup.string()
    .min(2, "*Must exceed 2 character")
    .max(100, "*Cannot exceed 100 characters")
    .required("*Please enter the practice name"),
  firstName: Yup.string()
    .min(2, "*Must exceed 2 character")
    .max(40, "*Cannot exceed 40 characters")
    .required("*This field is Required"),
  lastName: Yup.string()
    .min(2, "*Must exceed 2 character")
    .max(40, "*Cannot exceed 40 characters")
    .required("*This field is Required"),
  npi: Yup.string()
    .min(10, "*Must be 10 digits")
    .max(10, "*Must be 10 digits")
    .required("*This field is Required"),
  contact: Yup.string()
    .required("Must enter a phone number, pattern 555-000-0000")
    .min(10, "Not enough characters, pattern 555-000-0000")
    .max(50, "Too many characters, pattern 555-000-0000")
    .matches(phoneRegExp, "*Phone number is not valid, pattern 555-000-0000"),
  email: Yup.string()
    .email("*Invalid email format")
    .max(40, "*Cannot exceed 40 characters")
    .required("*Required field"),
  password: Yup.string()
    .required("Please enter your password")
    .matches(
      /^.*(?=.{8,})((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$/,
      "Password must contain at least 8 characters, one uppercase, one number and one special case character"
    ),
  confirmPassword: Yup.string().required("Please enter your password"),
});

export default providerFormSchema;
