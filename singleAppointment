import React from "react";
import { Avatar, Button, ListItem } from "@material-ui/core";
import PropTypes from "prop-types";
import debug from "sabio-debug";
const _logger = debug.extend("SingleAppointment");

const SingleAppointment = (props) => {
  function handleView() {
    const appointmentId = props.appointment.id;
    props.viewAppointment(appointmentId);
  }

  _logger(props)
  
  return (
    <ListItem className=" justify-content-between  py-3">
      <div className="d-flex align-items-center w-10">
        <Avatar
          alt="..."
          src={props.appointment.userProfile.avatarUrl}
          className="mr-2"
        />
        <div className="text-black  ">
          {`${props.appointment.userProfile.firstName} ${props.appointment.userProfile.lastName}`}
        </div>

      </div>
      <div
        className="padding-for-mobile-text  padding-for-laptop-title  item-padding-for-tablet
      remove-margin-for-laptop"
      >
        {" "}
        <span className="text-black">{props.appointment.startTime}</span>
      </div>
      <div
        className="remove-for-mobile remove-for-laptop "
        style={({ minWidth: "150px" }, { textAlign: "center" })}
      >
        {" "}
        <span className="text-black">
          {props.appointment.medicalService.name}
        </span>
      </div>
      <div className="padding-for-mobile-text padding-for-laptop-title item-padding-for-tablet
      remove-margin-for-laptop">
        {" "}
        <span className={(props.appointment.isConfirmed ? 'badge badge-success' : 'badge badge-danger')}>{(props.appointment.isConfirmed ? 'Confirmed' : 'UnConfirmed')}</span>
      </div>
      <Button
        variant="contained"
        color="secondary"
        onClick={() =>
          props.sendSMS({
            phone: "+13236387690",
            customerName: `${props.appointment.userProfile.firstName} ${props.appointment.userProfile.lastName}`,
            businessName: "Jeff Sherman Clinic",
            dateTime: `${props.appointment.startTime}`,
          })
        }
      >
        Send
      </Button>
      <Button
        size="small"
        variant="outlined"
        color="primary"
        className="ml-4"
        onClick={handleView}
      >
        View
      </Button>
    </ListItem>
  );
};

SingleAppointment.propTypes = {
  appointment: PropTypes.shape({
    dateCreated: PropTypes.string.isRequired,
    dateModified: PropTypes.string.isRequired,
    id: PropTypes.number.isRequired.isRequired,
    isConfirmed: PropTypes.bool.isRequired,
    providerId: PropTypes.number.isRequired,
    startTime: PropTypes.string.isRequired,
    medicalService: PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
      cpt4Code: PropTypes.string,
    }),
    medicalServiceType: PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
    }),
    userProfile: PropTypes.shape({
      avatarUrl: PropTypes.string.isRequired,
      dateCreated: PropTypes.string.isRequired,
      dateModified: PropTypes.string.isRequired,
      firstName: PropTypes.string.isRequired,
      id: PropTypes.number,
      lastName: PropTypes.string.isRequired,
      mi: PropTypes.string,
      userId: PropTypes.number.isRequired,
    }),
  }),
  viewAppointment: PropTypes.func.isRequired,
  sendSMS: PropTypes.func.isRequired,
};
export default SingleAppointment;
