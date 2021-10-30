import * as Yup from "yup";

const practiceLocationsSchema =

Yup.array().of(Yup.object().shape  // Yup.array().of(Yup.object().shape   )   Yup.object().shape 
({ 
	tempLocationId: Yup.number()
        .required("Must enter Temp Location Id")
        .integer()
        .min(1, "number must be 1 or larger"),
    locationTypeId: Yup.number()    
        .required("Must enter a Location Type")
        .integer()
        .min(1, "number must be 1 or larger"),
    lineOne: Yup.string()
		.min(2, "not enough characters")
		.max(255, "too many characters")
        .required("Must enter line one of address"),
    lineTwo: Yup.string()
		.min(2, "not enough characters")
        .max(255, "too many characters"),
    city: Yup.string()
		.min(2, "not enough characters")
		.max(255, "too many characters")
        .required("Must enter a city"),
    zip: Yup.string()
		.min(2, "not enough characters")
        .max(50, "too many characters"),
    stateId: Yup.number()    
        .required("Must select a state")
        .integer()
        .min(1, "Must select a state")
        .max(51, "invalid number selection"),
    latitude: Yup.number()  
        .required("Must enter latitude")   
        .min(-90.00000, "number must be more than -90.00")
        .max(90.00000, "number must be less than 90.00"),
    longitude: Yup.number()    
        .required("Must enter longitude")   
        .min(-180.00000, "number must be more than -180.00")
        .max(180.00000, "number must be less than 180.00"),
}) )

export  { practiceLocationsSchema } ;
