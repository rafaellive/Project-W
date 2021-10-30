import * as Yup from "yup";

const certificationDetailsSchema =
  
  Yup.object().shape({
    certifications: Yup.array().of(
        Yup.object().shape({
            certificationId: Yup.number()
                .required("Must select a certification")
                .integer()
                .min(1, "Must select a certification"),
        })
    ).required("Must select at least one certification"),
})

export  {certificationDetailsSchema};
