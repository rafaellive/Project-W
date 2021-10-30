import * as Yup from "yup";

const affiliationDetailsSchema =
  
  Yup.object().shape({
    affiliations: Yup.array().of(
        Yup.object().shape({
            name: Yup.string()
                .required("Please enter a affiliation name")
                .min(2,"Not enough characters")
                .max(500,"Too many characters"),
            affiliationTypeId: Yup.number()
                .required("Must select an affiliation type")
                .integer()
                .min(1, "Number must be 1 or larger"),
            dateExpired: Yup.date()
        })
    ).required(),
})

export  {affiliationDetailsSchema};
