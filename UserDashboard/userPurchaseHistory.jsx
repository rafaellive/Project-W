import React from "react";
import PropTypes from "prop-types";

import userDashService from "@services/users/userDashService";
import userService from "@services/userService";
import practiceService from "@services/practiceService";

import { withStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import Backdrop from "@material-ui/core/Backdrop";
import Fade from "@material-ui/core/Fade";
import FullCalendar from "@fullcalendar/react";
import listPlugin from "@fullcalendar/list";

import debug from "sabio-debug";
const _logger = debug.extend("UserPurchaseHistory");

const styles = (theme) => ({
  modal: {
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  paper: {
    backgroundColor: theme.palette.background.paper,
    border: "2px solid #000",
    boxShadow: theme.shadows[5],
    padding: theme.spacing(2, 4, 3),
  },
});


class UserPurchaseHistory extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      modal: false,
      appointmentDetails: {},
      arrayAppointmentsWithPractice: [],
    };
  }
  componentDidMount() {
    this.getCurrentUserIdAndPopulateAppointments();
  }

  getCurrentUserIdAndPopulateAppointments = () => {
    userService
      .getCurrentUser()
      .then(this.getCurrentUserSuccess)
      .catch(this.getCurrentUserError);
  };

  getCurrentUserSuccess = (response) => {
    const user = response.item;
    _logger(user);
    this.getAppointementsByUserId(user.id);
  };

  getCurrentUserError = (errResponse) => {
    _logger({ error: errResponse.config });
  };

  getAppointementsByUserId = (userId) => {
    userDashService
      .getAllAppointments(userId)
      .then(this.getAllAppointmentsSucess)
      .then(this.setStateWithUserId(userId))
      .catch(this.getAllAppointmentsError);
  };

  getAllAppointmentsSucess = (response) => {
    const appointments = response.items;
    _logger(appointments);
    this.getPracticesPerAppointment(appointments);
    this.setState((prevState) => {
      return {
        appointments,
      };
    });
  };

  getAllAppointmentsError = (errResponse) => {
    _logger({ error: errResponse.config });
  };

  setStateWithUserId = (userId) => {
    this.setState((prevState) => {
      return {
        userId,
      };
    });
  };

  getPracticesPerAppointment = (appointments) => {
    var arrayAppointmentsWithPractice = [];
    for (let index = 0; index < appointments.length; index++) {
      const appointment = appointments[index];
      practiceService
        .getPracticeByAppointmentId(appointment.appointmentId)
        .then(this.onGetPracticeByAppointmentIdSuccess)
        .then(
          this.getArrayOfAppointmentsWithPractice(
            appointment,
            arrayAppointmentsWithPractice
          )
        )
        .then(
          this.setStateWithArrayOfAppointmentsWithPractice(
            arrayAppointmentsWithPractice
          )
        )
        .catch(this.onGetPracticeByAppointmentIdSuccess);
    }
  };

  onGetPracticeByAppointmentIdSuccess = (response) => {
    const practice = response.item;
    _logger(practice);
    return { practice };
  };

  getAllAppointmentsError = (errResponse) => {
    _logger({ error: errResponse.config });
  };

  getArrayOfAppointmentsWithPractice = (
    appointment,
    arrayAppointmentsWithPractice
  ) => {
    return (result) => {
      if (appointment.appointmentId === result.practice.appointmentId) {
        var appointmentWithPractice = { ...appointment, ...result.practice };
        _logger(appointmentWithPractice);

        arrayAppointmentsWithPractice.push(appointmentWithPractice);
      }
      return {
        arrayAppointmentsWithPractice,
      };
    };
  };

  setStateWithArrayOfAppointmentsWithPractice = (
    arrayAppointmentsWithPractice
  ) => {
    return (result) => {
      this.setState((prevState) => {
        return {
          arrayAppointmentsWithPractice: result.arrayAppointmentsWithPractice,
          mappedAppointmentsListView: result.arrayAppointmentsWithPractice.map(
            this.mapAppointmentsListView
          ),
        };
      });
    };
  };

  mapAppointmentsListView = (oneAppointment) => {
    var serviceTitle = "";
    var totalPrice = 0;
    if (oneAppointment.providerServices.length > 1) {
      for (
        let index = 0;
        index < oneAppointment.providerServices.length;
        index++
      ) {
        const appointmentService = oneAppointment.providerServices[index];
        if (index === oneAppointment.providerServices.length - 1) {
          serviceTitle += appointmentService.medicalService.name;
          totalPrice += appointmentService.price;
        } else {
          serviceTitle += appointmentService.medicalService.name + ", ";
          totalPrice += appointmentService.price;
        }
      }
    } else {
      serviceTitle = oneAppointment.providerServices[0].medicalService.name;
    }
    var appointmentListView = {};
    appointmentListView.title = serviceTitle;
    appointmentListView.start = oneAppointment.appointmentStart;
    appointmentListView.end = oneAppointment.appointmentEnd;
    appointmentListView.totalPrice = totalPrice;
    appointmentListView.appointmentId = oneAppointment.appointmentId;
    return appointmentListView;
  };

  handleEventClick = (e) => {
    _logger(e);
    var appointmentId = e.event.extendedProps.appointmentId;
    var appointmentPrice = e.event.extendedProps.totalPrice;
    _logger(appointmentId, appointmentPrice);
    this.handleOpen(appointmentId, appointmentPrice);
  };

  handleOpen = (appointmentId, appointmentPrice) => {
    var extractedAppointmentInformation = {};

    this.state.arrayAppointmentsWithPractice.forEach((appointment) => {
      if (appointmentId === appointment.appointmentId) {
        const start = new Date(appointment.appointmentStart).toLocaleString();
        const services = appointment.providerServices.map((providerService) =>
        <div key={providerService.id}>
            <p>{providerService.medicalService.name + ':' + ' ' + '$' + providerService.price}</p>
        </div>
        );
        extractedAppointmentInformation.practiceName = appointment.practiceName;
        extractedAppointmentInformation.address =
          appointment.lineOne +
          " " +
          (appointment?.lineTwo || "") +
          `\n ` +
          appointment.city +
          ", " +
          appointment.stateCode +
          " " +
          appointment.zip;
        extractedAppointmentInformation.phone = appointment.phone;
        extractedAppointmentInformation.fax = appointment.fax;
        extractedAppointmentInformation.email = appointment.email;
        extractedAppointmentInformation.siteUrl = appointment?.siteUrl || "";
        extractedAppointmentInformation.services = services;
        extractedAppointmentInformation.start = start;
        extractedAppointmentInformation.price = appointmentPrice || "";
      }
    });
    this.setState(() => {
      return {
        modal: !this.state.modal,
        appointmentDetails: extractedAppointmentInformation,
      };
    });
  };

  handleClose = () => {
    this.setState({ modal: !this.state.modal });
  };

  render() {
    const { classes } = this.props;
    return (
      <React.Fragment>
          <div>
              <h3 className="text-center">Purchase History</h3>
          </div>
        <FullCalendar
        height="45%"
          plugins={[listPlugin]}
          initialView="listYear"
          headerToolbar={{
            left: "prev,next",
            right: "title",
          }}
          events={this.state?.mappedAppointmentsListView}
          eventClick={this.handleEventClick}
        />
        <Modal
          aria-labelledby="transition-modal-title"
          aria-describedby="transition-modal-description"
          className={classes.modal}
          open={this.state.modal}
          onClose={this.handleClose}
          closeAfterTransition
          BackdropComponent={Backdrop}
          BackdropProps={{
            timeout: 500,
          }}
        >
          <Fade in={this.state.modal}>
            <div className={classes.paper}>
            <img src="https://sabio-training.s3-us-west-2.amazonaws.com/welrus/8f58cbc1-db26-4e2c-82d3-975c5616c488-welrus_logotype_3.2.png"
                         alt="Welrus logo"
                         width="162"
                         height="50"></img>
                         <br></br>
              <h4 className="text-center">Appointment Info</h4>
              <p className="float-left" style={{fontWeight: 'bold'}}>
                Date:
              </p>
              <p className="float-right">
                {this.state.appointmentDetails.start}
              </p>
              <br></br>
              <br></br>
              <p style={{fontWeight: 'bold'}}>
                Location:
              </p>
              <h3 id="appointment-practice-name">
                {this.state.appointmentDetails.practiceName}
              </h3>
              <p id="appointment-address">
                {this.state.appointmentDetails.address}
              </p>
              <p id="appointment-address">
                {this.state.appointmentDetails.email}
              </p>
              <p id="appointment-fax">
                Fax: {this.state.appointmentDetails.fax}
              </p>
              <p id="appointment-phone">
                Phone: {this.state.appointmentDetails.phone}
              </p>
              <p id="appointment-site-url">
                Site URL: {this.state.appointmentDetails.siteUrl}
              </p>
              <p style={{fontWeight: 'bold'}}>
                Services Booked:
              </p>
              <p className="text-right" style={{paddingRight: '40px', borderBottomStyle: "groove"}} id="appointment-service">
                {this.state.appointmentDetails.services}
              </p>
              <p className="text-right" style={{fontWeight: 'bold', paddingRight: '90px'}}>
                Total:
              </p>
              <p className="text-right" style={{paddingRight: '40px'}} id="appointment-price">
                ${this.state.appointmentDetails.price}
              </p>
            </div>
          </Fade>
        </Modal>
      </React.Fragment>
    );
  }
}
UserPurchaseHistory.propTypes = {
  classes: PropTypes.shape({
    modal: PropTypes.shape({
      display: PropTypes.string.isRequired,
      alignItems: PropTypes.string.isRequired,
      justifyContent: PropTypes.string.isRequired,
    }),
    paper: PropTypes.shape({
      backgroundColor: PropTypes.string.isRequired,
      border: PropTypes.string.isRequired,
      boxShadow: PropTypes.number.isRequired,
      padding: PropTypes.number.isRequired,
    }),
  }),
};

export default withStyles(styles, { withTheme: true })(UserPurchaseHistory);
