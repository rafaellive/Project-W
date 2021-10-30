import React from "react";
import providerServiceService from "@services/providerServiceService";
import medicalServiceService from "@services/medicalServiceService";
import medicalServiceTypeService from "@services/medicalServiceTypeService";
import scheduleService from "@services/scheduleService";
import PropTypes from "prop-types";
import AsyncSelect from "react-select/async";

import {
  Grid,
  Container,
  Input,
  InputLabel,
  Card,
  Button,
  FormControl,
} from "@material-ui/core";
import { Formik, Field } from "formik";
import Select from "@material-ui/core/Select";

import providerServiceSchema from "../../../schema/providerServiceSchema";

import debug from "sabio-debug";
const _logger = debug.extend("ProviderServiceAddForm");

class providerServiceAddForm extends React.Component {
  state = {
    providerServiceFormData: {
      price: 0.0,
      selectedMedicalService: [],
      selectedMedicalServiceType: [],
    },
    medicalServiceId: 0,
    medicalServiceTypeId: 0,
  };

  componentDidMount() {
    this.setPracticeDropDownOptions(
      this.props?.history?.location?.state?.providerId
    );
    _logger(this.props.location.state.providerId);
  }

  setPracticeDropDownOptions = (providerId) => {
    scheduleService
      .getPracticesWithScheduleIdByProviderId(providerId)
      .then(this.onGetPracticesSuccess)
      .catch(this.onGetPracticesError);
  };

  onGetPracticesSuccess = (response) => {
    const practices = response.items;
    this.setState((prevState) => {
      return {
        mappedPractices: practices.map(this.mapPracticeDropDownOptions),
      };
    });
  };

  onGetPracticesError = (errResponse) => {
    _logger({ error: errResponse.config });
  };

  mapPracticeDropDownOptions = (onePractice) => {
    return (
      <option value={onePractice.scheduleId}>{onePractice.practiceName}</option>
    );
  };

  onChangeMedicalService = (selectedMedicalService) => {
    this.setState({
      medicalService: selectedMedicalService,
    });
  };

  onChangeMedicalServiceType = (selectedMedicalServiceType) => {
    this.setState({
      medicalServiceType: selectedMedicalServiceType,
    });
  };

  handleSubmit = (values, { resetForm }) => {
    var providerServiceFormValues = { ...values };
    var providerServiceFormPayload = {};
    providerServiceFormPayload.scheduleId = parseInt(
      providerServiceFormValues.scheduleId
    );
    providerServiceFormPayload.providerId = this.props.location.state.providerId;
    providerServiceFormPayload.price = providerServiceFormValues.price;
    providerServiceFormPayload.serviceid = this.state.medicalServiceId;
    providerServiceFormPayload.servicetypeid = this.state.medicalServiceTypeId;
    providerServiceService
      .add(providerServiceFormPayload)
      .then(this.onAddProviderServiceSuccess)
      .catch(this.onAddProviderServiceError);

    this.props.history.push("/provider/services/");
  };

  onAddProviderServiceSuccess = (response) => {
    _logger({ providerService: response });
  };

  onAddProviderServiceError = (errResponse) => {
    _logger({ error: errResponse.config });
  };

  filterMedicalServices = (inputString, callback) => {
    medicalServiceService
      .getAllByKeyword(inputString)
      .then((response) =>
        this.onGetAllServicesByKeywordSucess(response, callback)
      )
      .catch((response) => this.onGetAllServicesByKeywordError);
  };

  filterMedicalServiceTypes = (inputString, callback) => {
    medicalServiceTypeService
      .getAllByKeyword(inputString)
      .then((response) =>
        this.onGetAllServiceTypesByKeywordSucess(response, callback)
      )
      .catch((response) => this.onGetAllServiceTypesByKeywordError);
  };

  promiseServiceOptions = (inputValue, callback) => {
    if (inputValue.length > 2) {
      new Promise((resolve) => {
        resolve(this.filterMedicalServices(inputValue, callback));
      });
    } else {
      return;
    }
  };

  promiseServiceTypeOptions = (inputValue, callback) => {
    if (inputValue.length > 2) {
      new Promise((resolve) => {
        resolve(this.filterMedicalServiceTypes(inputValue, callback));
      });
    } else {
      return;
    }
  };

  onGetAllServicesByKeywordSucess = (response, callback) => {
    const medicalServiceList = response.items;
    let data = medicalServiceList.map((item) => {
      return {
        label: item.name,
        value: item.id,
      };
    });
    _logger({ data });
    callback(data);
  };

  onGetAllServicesByKeywordError = (errResponse) => {
    _logger({ error: errResponse.config });
    const error = errResponse.config;
  };

  onGetAllServiceTypesByKeywordSucess = (response, callback) => {
    const medicalServiceTypeList = response.items;
    let data = medicalServiceTypeList.map((item) => {
      return {
        label: item.name,
        value: item.id,
      };
    });
    _logger({ data });
    callback(data);
  };

  onGetAllServiceTypesByKeywordError = (errResponse) => {
    _logger({ error: errResponse.config });
    const error = errResponse.config;
  };

  handleMedService = (selected, setFieldValue) => {
    _logger(selected, "--------handleMedService--------");
    setFieldValue("selectedMedicalService", selected.label);
    var medicalServiceValue = selected.value;
    this.setState({
      medicalServiceId: medicalServiceValue,
    });
  };

  handleMedServiceType = (selected, setFieldValue) => {
    _logger(selected, "--------handleMedServiceType--------");
    setFieldValue("selectedMedicalServiceType", selected.label);
    var medicalServiceTypeValue = selected.value;
    this.setState({
      medicalServiceTypeId: medicalServiceTypeValue,
    });
  };

  render() {
    return (
      <React.Fragment>
        <Formik
          enableReinitialize={true}
          validationSchema={providerServiceSchema}
          initialValues={this.state.providerServiceFormData}
          onSubmit={this.handleSubmit}
        >
          {(props) => {
            const {
              values,
              touched,
              errors,
              handleSubmit,
              handleChange,
              isValid,
              isSubmitting,
              setFieldValue,
            } = props;
            return (
              <Container>
                <Grid container justify="center" alignItems="center">
                  <Grid item xs={6}>
                    <Card
                      justify="center"
                      className="p-4 mb-4"
                      style={{ height: "500px" }}
                    >
                      <div className="font-size-lg font-weight-bold">
                        Add a Medical Service
                      </div>
                      <form
                        className="px-5"
                        noValidate
                        autoComplete="off"
                        onSubmit={handleSubmit}
                      >
                        <br></br>
                        <div>
                          <FormControl variant="outlined" fullWidth>
                            <InputLabel htmlFor="outlined-practice-native-simple">
                              Practice
                            </InputLabel>
                            <Select
                              native
                              values={values.scheduleId}
                              onChange={handleChange}
                              label="Practice"
                              inputProps={{
                                name: "scheduleId",
                                id: "outlined-practice-native-simple",
                              }}
                            >
                              <option aria-label="None" value="" />
                              {this.state.mappedPractices}
                            </Select>
                          </FormControl>
                        </div>
                        <div>
                          <FormControl
                            className="m-2"
                            fullWidth
                            label="Standard"
                            error={errors.price && touched.price}
                          >
                            <InputLabel>Price</InputLabel>
                            <Input
                              name="price"
                              step="any"
                              type="number"
                              values={values.price}
                              placeholder="Enter price"
                              autoComplete="off"
                              onChange={handleChange}
                              id="price"
                            />
                          </FormControl>
                        </div>
                        <div>
                          <FormControl
                            className="m-2"
                            fullWidth
                            label="Standard"
                            error={
                              errors.selectedMedicalService &&
                              touched.selectedMedicalService
                            }
                          >
                            <InputLabel>Service</InputLabel>
                            <AsyncSelect
                              name="selectedMedicalService"
                              values={values.selectedMedicalService}
                              onChange={(option) =>
                                this.handleMedService(option, setFieldValue)
                              }
                              placeholder="Please select one medical service... "
                              cacheOptions
                              loadOptions={this.promiseServiceOptions}
                            />
                          </FormControl>
                        </div>
                        <div>
                          <FormControl
                            className="m-2"
                            fullWidth
                            label="Standard"
                            error={
                              errors.selectedMedicalServiceType &&
                              touched.selectedMedicalServiceType
                            }
                          >
                            <InputLabel>Service Type</InputLabel>
                            <AsyncSelect
                              name="selectedMedicalServiceType"
                              values={values.selectedMedicalServiceType}
                              onChange={(option) =>
                                this.handleMedServiceType(option, setFieldValue)
                              }
                              placeholder="Please select one medical service type... "
                              cacheOptions
                              loadOptions={this.promiseServiceTypeOptions}
                              styles={{ color: "red" }}
                            />
                          </FormControl>
                        </div>
                        <Button
                          type="submit"
                          className="btn btn-primary"
                          value="Submit"
                          id="submitProviderService"
                          disabled={isSubmitting}
                        >
                          Submit
                        </Button>
                      </form>
                    </Card>
                  </Grid>
                </Grid>
              </Container>
            );
          }}
        </Formik>
      </React.Fragment>
    );
  }
}

providerServiceAddForm.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func,
    location: PropTypes.shape({
      state: PropTypes.shape({
        providerId: PropTypes.number.isRequired,
      }),
    }),
  }).isRequired,
  location: PropTypes.shape({
    state: PropTypes.shape({
      providerId: PropTypes.number.isRequired,
    }),
  }),
};

export default providerServiceAddForm;
