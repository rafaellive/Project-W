import * as Yup from "yup";

const phoneRegExp = /^((\\([0-9]{3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/

const providerDetailsSchema=

Yup.object().shape
({ 
	phone: Yup.string()
		.min(10, "not enough characters")
		.max(50, "too many characters")
        .required("Must enter phone")
        .matches(phoneRegExp, 'Phone number is not valid, pattern 555-000-0000'),
    fax: Yup.string()    
		.min(10, "not enough characters")
		.max(50, "too many characters")
        .required("Must enter fax")
        .matches(phoneRegExp, 'Phone number is not valid, pattern 555-000-0000'),
    networks: Yup.string()
		.min(2, "not enough characters")
		.max(2000, "too many characters")
		.required("Must enter networks"),
	npi: Yup.string() 
        .required("Must enter NPI number")
        .min(10, "too few digits")
        .max(10, "too many digits"),
    genderTypeId: Yup.number()
        .integer()
        .required("Must select one of the options for gender type"),
    titleTypeId: Yup.number()
        .integer()
        .required("Must select one of the options for title"),
    userProfileId: Yup.number()
        .integer(),
    isAccepting: Yup.bool()
            .required("Are you accepting new patients?")
}).required();

export  { providerDetailsSchema } ;
