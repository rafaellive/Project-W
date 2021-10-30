import * as Yup from "yup";

const phoneRegExp = /^((\\([0-9]{3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/

const practiceDetailsSchema =

Yup.object().shape  
({ 
	tempLocationId: Yup.number()
        .required("Must enter Temp Location Id")
        .integer()
        .min(1, "Please select a location"),
    name: Yup.string()
        .min(2, "Not enough characters")
        .max(100, "Too many characters")
        .nullable(),
    facilityTypeId: Yup.number()    
        .required("Must select a Facility Type")
        .integer()
        .min(1, "Please select a facility type"),
    scheduleId: Yup.number()
        .integer()
        .min(1, "Number must be 1 or larger")
        .nullable(),
    phone: Yup.string()
        .required("Must enter a phone number, pattern 555-000-0000")
        .min(10, "Not enough characters, pattern 555-000-0000")
        .max(50, "Too many characters, pattern 555-000-0000")
        .matches(phoneRegExp, 'Phone number is not valid, pattern 555-000-0000'),
    fax: Yup.string()
        .min(10, "Not enough characters, pattern 555-000-0000")
        .max(50, "Too many characters, pattern 555-000-0000")
        .nullable()
        .matches(phoneRegExp, 'Phone number is not valid, pattern 555-000-0000'),
    email: Yup.string()
        .min(2, "Not enough characters")
        .max(255, "Too many characters")
        .email()
        .nullable(),
    siteUrl: Yup.string()
        .min(2, "Not enough characters")
        .max(200, "Too many characters")
        .url()
        .nullable(),
    adaAccessible: Yup.bool()
        .required("Is your facility ADA accessible?"),
    insuranceAccepted: Yup.bool()
        .required("Missing a response for insurance accepted"),
    genderAccepted: Yup.number()    
        .integer()
        .min(0, "Number must be 0 or larger")
        .nullable(),
}) 

export  { practiceDetailsSchema } ;
