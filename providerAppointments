import React from "react";
import { Grid, Card, List, ListItem } from "@material-ui/core";
import PerfectScrollbar from "react-perfect-scrollbar";
import SingleAppointment from "./SingleAppointment";
import PropTypes from "prop-types";
import { Snackbar, Button } from "@material-ui/core";
import Alert from "../../../assets/components/Alert";
import appointmentService from "@services/appointmentService";
import smsService from "../../../services/smsService";
import debug from "sabio-debug";

const _logger = debug.extend("ProviderAppointments");

class ProviderAppointments extends React.Component {
  state = {
    isViewOpen: false,
    providerId: 0,
    searchValues: "",
    snackBarShow: false,
    severity: "error",
    barMessage: "",
  };

  componentDidUpdate(prevProps) {
    let id = this.props.providerId;
    if (id !== prevProps.providerId) {
      this.getAppointmentsByProviderId();
    }
    _logger("appointments component finished updating...........");
  }

  getAppointmentsByProviderId = () => {
    let id = this.props.providerId;
    _logger(id);
    if (id) {
      appointmentService
        .getAllByProviderId(id)
        .then(this.onGetAllSuccess)
        .catch(this.onGetAllError);
    }
  };

  onGetAllSuccess = (response) => {
    let apptArray = response.data.items;
    let newAppointmentArray = [];
    _logger({ success: apptArray });

    apptArray.forEach((appointment) => {
      let todaysDate = new Date();
      todaysDate = todaysDate.toDateString();

      let apppointmentDate = new Date(appointment.startTime);
      apppointmentDate = apppointmentDate.toDateString();

      if (todaysDate === apppointmentDate) {
        newAppointmentArray.push(appointment);
      }
    });

    if (newAppointmentArray !== null) {
      this.setState(() => {
        return {
          mappedAppointments: newAppointmentArray.map(this.mapAppointments),
          snackBarShow: true,
          barMessage: `You have ${newAppointmentArray.length} appointments today.`,
          severity: "success",
        };
      }, _logger("setState finished for render all appointments..........."));
    }
  };
  onGetAllError = (error) => {
    _logger({ error: error });
    this.setState((prevState) => {
      return {
        ...prevState,
        snackBarShow: true,
        barMessage: "Error retrieving appointments for today.",
        severity: "error",
      };
    });
  };

  mapAppointments = (myAppointment) => {
    let apppointmentDate = new Date(myAppointment.startTime);
    let appointmentTime = apppointmentDate
      .toLocaleTimeString()
      .replace(/(.*)\D\d+/, "$1");
    myAppointment.startTime = appointmentTime;

    return (
      <SingleAppointment
        appointment={myAppointment}
        key={myAppointment.id}
        viewAppointment={this.props.viewAppointment}
        sendSMS={this.sendSMS}
      />
    );
  };
  closeNotification = () => {
    this.setState((prevState) => {
      return {
        snackBarShow: !prevState.snackBarShow,
      };
    });
  };

  sendSMS = ({ phone, customerName, businessName, dateTime }) => {
    const payload = {
      phone,
      customerName,
      businessName,
      dateTime,
    };
    smsService
      .sendSMS(payload)
      .then(this.sendSMSSuccess)
      .catch(this.sendSMSError);
  };

  sendSMSSuccess = (response) => {
    _logger("appointment reminder success", response);
    this.setState((prevState) => {
      return {
        ...prevState,
        snackBarShow: true,
        barMessage: "Appointment reminder has been sent!",
        severity: "success",
      };
    });
  };

  sendSMSError = (error) => {
    _logger("appointment reminder error", error);
    this.setState((prevState) => {
      return {
        ...prevState,
        snackBarShow: true,
        barMessage: "Not able to send apppointment reminder.",
        severity: "error",
      };
    });
  };

  render() {
    _logger("rendering...........");
    return (
      <Grid item xs={12} md={6} lg={6}>
        <Card className="card-box overflow-hidden  text-dark card-width-for-mobile">
          <div
            className=" text-black card-header--title py-2 font-size-lg font-weight-bold"
            style={{ textAlignLast: "center" }}
          >
            {" "}
            Daily Appointments
          </div>
          <div className="scroll-area" style={{ height: "760px" }}>
            <PerfectScrollbar>
              <List className="py-0">
                <ListItem
                  className="d-flex justify-content-between align-items-center py-2 bg-brand-facebook"
                  style={{ borderBottomStyle: "groove" }}
                >
                  <div className="d-flex align-items-center">
                    <div className="font-size-lg pb-0 text-white d-block mb-0">
                      Patient Name
                    </div>
                  </div>
                  <div className="padding-for-mobile-title padding-for-tablet-title">
                    <span
                      className="font-size-lg pb-1  text-white d-block mb-1"
                      style={{ paddingLeft: "5px" }}
                    >
                      Time
                    </span>
                  </div>
                  <div className="padding-for-mobile-title">
                    {" "}
                    <span
                      className="font-size-lg pb-1  text-white d-block mb-1"
                      style={{ paddingLeft: "5px" }}
                    >
                      Type
                    </span>
                  </div>

                  <div className="remove-for-mobile remove-for-laptop">
                    {" "}
                    <span
                      className="font-size-lg text-white d-block"
                      style={{ marginRight: "5px" }}
                    >
                      Status
                    </span>
                  </div>
                  <div className="padding-for-mobile-title">
                    {" "}
                    <span
                      className="font-size-lg pb-1  text-white d-block mb-1"
                      style={{ paddingLeft: "15px" }}
                    >
                      Reminder
                    </span>
                  </div>
                  <div></div>
                </ListItem>

                <Snackbar
                  open={this.state.snackBarShow}
                  autoHideDuration={6000}
                  anchorOrigin={{ vertical: "top", horizontal: "right" }}
                  onClose={this.closeNotification}
                >
                  <Alert severity={this.state.severity}>
                    {this.state.barMessage}
                  </Alert>
                </Snackbar>

                {this.state.mappedAppointments ? (
                  this.state.mappedAppointments
                ) : (
                  // -----------------------------test code------------------------------
                  <ListItem className="d-flex justify-content-between align-items-center py-3">
                    {/* <div>
                      {" "}
                      <span className="text-black d-block mt-2 font-size-lg pb-1">
                        No appointments scheduled today.
                      </span>
                    </div> */}
                    {/* -----------------test  code------------- */}
                    <div>
                      {" "}
                      <span className="text-black d-block mt-2 font-size-lg pb-1">
                        Robbie Riggle
                      </span>
                    </div>
                    <div>
                      {" "}
                      <span className="text-black d-block mt-2 font-size-lg pb-1">
                        2021-03-12
                      </span>
                    </div>
                    <div>
                      {" "}
                      <span className="text-black d-block mt-2 font-size-lg pb-1"></span>
                    </div>
                    <div>
                      {" "}
                      <span className="text-black d-block mt-2 font-size-lg pb-1">
                        <Button
                          variant="contained"
                          color="secondary"
                          onClick={() =>
                            this.sendSMS({
                              phone: "+13236387690",
                              customerName: "Robbie Riggle",
                              businessName: "Jeff Sherman clinic",
                              dateTime: "2021-03-12",
                            })
                          }
                        >
                          Send
                        </Button>
                      </span>
                    </div>
                  </ListItem>
                )}
              </List>
            </PerfectScrollbar>
          </div>
        </Card>
      </Grid>
    );
  }
}

ProviderAppointments.propTypes = {
  viewAppointment: PropTypes.func.isRequired,
  providerId: PropTypes.number.isRequired,
};

export default ProviderAppointments;
