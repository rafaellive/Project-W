import React, { Fragment } from "react";
import { TextField } from "@material-ui/core";
import debug from "sabio-debug";
import Grid from "@material-ui/core/Grid";
import Paper from "@material-ui/core/Paper";
import { Formik, Form } from "formik";
import userProfileSchema from "../../schema/userProfileSchema";
import { Button } from "@material-ui/core";
import PropTypes from "prop-types";
import userProfileService from "../../services/users/userProfileService";
import Snackbar from "@material-ui/core/Snackbar";
import Alert from "../../assets/components/Alert";
import FileUploader from "../files/FileUpload";

const _logger = debug.extend("UserProfileForm");

class UserProfileForm extends React.Component {
  state = {
    formData: {
      firstName: "",
      mi: "",
      lastName: "",
      avatarUrl: "",
      phoneNumber: "",
    },
    snackbarOpen: false,
    severity: "success",
    barMessage: "",
    message: null,
  };

  componentDidMount() {
    if (
      this.props.location.state &&
      this.props.location.state.type === "USER_PROFILE_EDIT"
    ) {
      _logger("is editing");
      const formData = this.props.location.state.userProfile;
      this.fillFormWith(formData);
    }
  }

  fillFormWith = (formData) => {
    this.setState((prevState) => {
      return { ...prevState, formData };
    });
    _logger(formData, "formData from componentDidMount");
  };

  handleSubmit = (values) => {
    if (values.id) {
      userProfileService
        .editUserProfile(values)
        .then(this.onEditSuccess)
        .catch(this.onEditError);
    } else {
      userProfileService
        .createUserProfile(values)
        .then(this.onCreateSuccess)
        .catch(this.onCreateError);
      var message = `Submitted form with the following values.
    ${JSON.stringify(values, null, 2)} `;
      _logger(message);
      this.setState({ message });
    }
  };

  onEditSuccess = () => {
    _logger("PUT successful");
    if (this.props?.currentUser?.roles) {
      let path = "/";
      const roles = this.props.currentUser.roles;
      if (roles.includes("Provider")) {
        path = "/provider/dashboard";
      } else if (roles.inlcudes("Customer")) {
        path = "/user/dashboard";
      } else if (roles.includes("Admin")) {
        path = "/dashboard/admin";
      }
      this.props.history.push(path);
    }
  };

  onEditError = (error) => {
    _logger("New profile error:", error);
    this.setState((prevState) => {
      return {
        ...prevState,
        snackbarOpen: true,
        barMessage: "Couldn't update profile",
        severity: "error",
      };
    });
  };

  uploadSuccess = (response, setFieldValue) => {
    _logger(response[0].url);
    const url = response[0].url;
    setFieldValue("avatarUrl", url);
  };

  onCreateSuccess = () => {
    _logger("PUT successful");
    this.props.history.push("/user/dashboard");
  };

  onCreateError = (error) => {
    _logger("New profile error:", error);
    this.setState((prevState) => {
      return {
        ...prevState,
        snackbarOpen: true,
        barMessage: "Couldn't create profile",
        severity: "error",
      };
    });
  };

  mapImage = (url) => {
    return <img src={url} />;
  };

  render() {
    _logger("rendering");

    return (
      <Fragment>
        <div style={{ paddingTop: 100 }}>
          <Grid
            container
            spacing={3}
            style={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <Formik
              enableReinitialize={true}
              validationSchema={userProfileSchema}
              initialValues={this.state.formData}
              onSubmit={this.handleSubmit}
            >
              {(props) => {
                const {
                  values,
                  touched,
                  errors,
                  setFieldValue,
                  handleSubmit,
                  handleChange,
                } = props;
                return (
                  <Grid item xs={12} sm={6}>
                    <Snackbar
                      open={this.state.snackbarOpen}
                      autoHideDuration={3000}
                      anchorOrigin={{
                        vertical: "center",
                        horizontal: "center",
                      }}
                    >
                      <Alert severity={this.state.severity}>
                        {this.state.barMessage}
                      </Alert>
                    </Snackbar>
                    <Paper style={{ textAlign: "center", flexGrow: 1 }}>
                      <Form
                        onSubmit={handleSubmit}
                        style={{ marginRight: "20px" }}
                      >
                        <div>
                          <TextField
                            fullWidth
                            label="First Name"
                            name="firstName"
                            type="text"
                            className="m-2"
                            onChange={handleChange}
                            value={values.firstName ? values.firstName : ""}
                            autoComplete="off"
                          />
                          {errors.firstName && touched.firstName && (
                            <span className="input-feedback text-danger">
                              {errors.firstName}
                            </span>
                          )}
                        </div>
                        <div>
                          <TextField
                            fullWidth
                            label="Middle Initial"
                            name="mi"
                            type="text"
                            className="m-2"
                            onChange={handleChange}
                            value={values.mi ? values.mi : ""}
                            autoComplete="off"
                          />
                          {errors.mi && touched.mi && (
                            <span className="input-feedback text-danger">
                              {errors.mi}
                            </span>
                          )}
                        </div>
                        <div>
                          <TextField
                            fullWidth
                            label="Last Name"
                            name="lastName"
                            type="text"
                            className="m-2"
                            onChange={handleChange}
                            value={values.lastName ? values.lastName : ""}
                            autoComplete="off"
                          />

                          {errors.lastName && touched.lastName && (
                            <span className="input-feedback text-danger">
                              {errors.lastName}
                            </span>
                          )}
                        </div>
                        <div>
                          <TextField
                            fullWidth
                            label="Phone Number"
                            name="phoneNumber"
                            type="text"
                            className="m-2"
                            onChange={handleChange}
                            value={values.phoneNumber ? values.phoneNumber : ""}
                            autoComplete="off"
                          />

                          {errors.phoneNumber && touched.phoneNumber && (
                            <span className="input-feedback text-danger">
                              {errors.phoneNumber}
                            </span>
                          )}
                        </div>
                        <FileUploader
                          onUploadSuccess={(response) =>
                            this.uploadSuccess(response, setFieldValue)
                          }
                          isMultiple={false}
                          buttonText="Upload Profile Photo"
                        />{" "}
                        {/* <div>  
                            {
                              values.avatarUrl.map(this.mapImage)
                            }
                          </div>   */}
                        <Button
                          variant="contained"
                          style={{
                            color: "white",
                            fontWeight: "bold",
                            marginLeft: "10px",
                            backgroundColor: "rgb(59 62 96)",
                            width: "100px",
                          }}
                          type="submit"
                        >
                          Submit
                        </Button>
                      </Form>
                    </Paper>
                  </Grid>
                );
              }}
            </Formik>
          </Grid>
        </div>
      </Fragment>
    );
  }
}

UserProfileForm.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func,
  }).isRequired,
  match: PropTypes.shape({
    params: PropTypes.shape({
      id: PropTypes.number,
    }),
  }),
  currentUser: PropTypes.shape({
    roles: PropTypes.arrayOf(PropTypes.string),
  }),
  location: PropTypes.shape({
    state: PropTypes.shape({
      type: PropTypes.string,
      userProfile: PropTypes.shape({
        id: PropTypes.number,
        userId: PropTypes.number,
        firstName: PropTypes.string.isRequired,
        mi: PropTypes.string,
        lastName: PropTypes.string.isRequired,
        avatarUrl: PropTypes.string,
      }),
    }),
  }),
};

export default UserProfileForm;
