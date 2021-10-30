import * as Yup from "yup";

Yup.addMethod(Yup.array, 'uniqueProperty', function (message, mapper= a=>a) {
    return this.test('unique', message, function (list) {
        return list.length  === new Set(list.map(mapper)).size;
    });
});

const licenseDetailsSchema =
  
  Yup.object().shape({
    licenses: Yup.array().of(
        Yup.object().shape({
            name: Yup.string()
                .required("Please enter a license name")
                .min(2,"Not enough characters")
                .max(100,"Too many characters"),
            licenseNumber: Yup.string()
                .required("Please enter a license number")
                .min(2,"Not enough characters")
                .max(50,"Too many characters"),
            licenseStateId: Yup.number()
                .required("Must select a state")
                .integer()
                .min(1, "Number must be 0 or larger"),
            dateExpired: Yup.date()
        })
    ).required()
    .uniqueProperty('Duplicate license number', 
        lic => lic.licenseNumber),
})

export  {licenseDetailsSchema};
