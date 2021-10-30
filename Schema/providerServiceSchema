import * as Yup from "yup";

const providerServiceSchema = Yup.object().shape({
  scheduleId: Yup.string().required("Required"),
  price: Yup.number()
    .test("is-decimal", "invalid decimal", (value) =>
      (value + "").match(/^\d*\.{1}\d*$/)
    )
    .required("Required"),
  selectedMedicalService: Yup.string().required("Required"),
  selectedMedicalServiceType: Yup.string().required("Required"),
});

export default providerServiceSchema;
