import * as Yup from "yup";

const providerLanguageDetailsSchema =
  
  Yup.object().shape({
    languages: Yup.array().of(
        Yup.object().shape({
            languageId: Yup.number()
                .required("Must select a language")
                .integer()
                .min(1, "Number must be 1 or larger"),
        })
    ).required(),
})

export  {providerLanguageDetailsSchema};
