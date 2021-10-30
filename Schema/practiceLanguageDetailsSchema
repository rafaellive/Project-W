import * as Yup from "yup";

const practiceLanguageDetailsSchema =
  
  Yup.object().shape({
    practiceLanguages: Yup.array().of(
        Yup.object().shape({
            languageId: Yup.number()
                .required("Must select a language")
                .integer()
                .min(1, "Number must be 1 or larger"),
            tempPracticeId: Yup.number()
                .required("Must have a tempPracticeId")
                .integer()
                .min(1, "Number must be 1 or larger"),
        })
    ).required(),
})

export  {practiceLanguageDetailsSchema};
