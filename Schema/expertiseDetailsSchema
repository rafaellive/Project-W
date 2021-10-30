import * as Yup from "yup";

const expertiseDetailsSchema =
  
  Yup.object().shape({
    expertise: Yup.array().of(
        Yup.object().shape({
            name: Yup.string()
                .required("Please enter a affiliation name")
                .min(2,"Not enough characters")
                .max(100,"Too many characters"),
            description: Yup.string()
                .required("Please enter a affiliation name")
                .min(2,"Not enough characters")
                .max(400,"Too many characters"),
        })
    ).required(),
})

export  {expertiseDetailsSchema};
