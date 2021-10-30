import * as Yup from "yup";

const specializationDetailsSchema =
  
  Yup.object().shape({
    specializations: Yup.array().of(
        Yup.object().shape({
            specializationId: Yup.number()
                .required("Must select a specialization")
                .integer()
                .min(1, "Must select a valid specialization"),
        })
    ),
})

export  {specializationDetailsSchema};
