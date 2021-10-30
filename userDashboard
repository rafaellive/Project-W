import React, { Fragment, useEffect } from 'react';
import PropTypes from 'prop-types';
import userProfileService from "../../services/users/userProfileService";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
  Grid,
  Card,
  CardContent,
  Button
} from '@material-ui/core';
import debug from "sabio-debug";
import UserAppointmentCal from './UserAppointmentCal';
import UserPurchaseHistory from './UserPurchaseHistory'
import userService from 'services/userService';
import userDashService from 'services/users/userDashService';


const _logger = debug.extend("UserDashboard");

const UserDashboard = (props) => {

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  const [value, setValue] = React.useState(null);
  const [profile, setProfile] = React.useState(
    props.location.state && props.location.state.type === "USER_PROFILE_VIEW"
      ? props.location.state.userProfile
      : null
  );

  const [userEmail, setUserEmail] = React.useState();

  const [appointmentCount, setAppointmentCount] = React.useState(0)

  const [upComingAppointment, setUpComingAppointment] = React.useState();

  useEffect(() => {
    if (!profile) {
      userProfileService.getCurrent().then(onGetSuccess).catch(onGetError);
    }
  }, [profile]);

  const onGetSuccess = (response) => {
    _logger(response, "getProfile response");
    setProfile(response.item);
    getUser(response)
  };

  const onGetError = (error) => {
    _logger(error);
  };

  const getUser = (response) => {
    let userId = response.item.userId;
    userService.getCurrentUser(userId)
    .then(onGetUserSuccess)
    .catch(onGetUserError)
  }

  const onGetUserSuccess = (response) => {
    _logger(response, "getUser response");
    setUserEmail(response.item.email);
    userDashService
    .getAllAppointments(response.item.id)
    .then(onGetAppointmentsSuccess)
    .catch(onGetAppointmentsError)
  };

  const onGetUserError = (error) => {
    _logger(error);
  };

  const onGetAppointmentsSuccess = (response) => {
    _logger(response, "getAllAppointments response")
    let appointments = response.items.length;
    setAppointmentCount(appointments)
  }
  const onGetAppointmentsError = (error) => {
    _logger(error);
  };

  const onClickEdit = () => {
    _logger("onClickEdit profile", profile);
    props.history.push(`/users/profiles/edit`, {
      type: "USER_PROFILE_EDIT",
      userProfile: profile,
    });
  };

  const defaultAvatarSrc =
  "https://st2.depositphotos.com/1009634/7235/v/950/depositphotos_72350117-stock-illustration-no-user-profile-picture-hand.jpg";

  return (
    <Fragment>
      <Grid container spacing={4}>
        <Grid item xs={12} lg={4}>
          <div className="bg-midnight-bloom p-3 rounded text-white h-100">
            <div className="d-flex align-items-start justify-content-between">
              <div className="avatar-icon-wrapper d-100">
                <span className="badge-circle badge badge-success">Online</span>
                <div className="avatar-icon d-100">
                  <img alt="..." src={profile ? profile.avatarUrl : defaultAvatarSrc} />
                </div>
              </div>
            </div>
            <div className="font-weight-bold font-size-lg d-flex align-items-center mt-2 mb-0">
              <span>{`${profile ? profile.firstName : "User"} ${
                      profile ? profile.lastName : "Name"
                    }`}</span>
            </div>
            <p className="mb-4 font-size-md text-white-50">{userEmail}</p>
            <Button
              size="medium"
              variant="contained"
              color="default"
              className="mr-3"
              onClick={onClickEdit}>
              Edit Profile
            </Button>

            <Card className="card-box mt-4 mb-4 bg-white text-light">
              <CardContent className="p-3">
                <div className="align-box-row align-items-start">
                  <div className="font-weight-bold">
                    <small className="text-black-50 d-block mb-1 text-uppercase">
                      Today&apos;s Appointments
                    </small>
                    <span className="font-size-md text-black mt-1">
                      No appointments scheduled for today.
                    </span>
                  </div>
                  <div className="ml-auto">
                  <div className="bg-midnight-bloom text-center d-flex align-items-center justify-content-center text-white font-size-xl d-50 rounded-circle">
                    <FontAwesomeIcon
                      icon={['far', 'calendar-check']}
                      className="font-size-lg text-white"
                    />
                    </div>
                  </div>
                </div>

              </CardContent>
            </Card>
            <div className="divider opacity-2 my-4" />
           
            <Card className="card-box mt-4 mb-4 bg-white text-light">
              <CardContent className="p-3">
                <div className="align-box-row align-items-start">
                  <div className="font-weight-bold">
                    <small className="text-black-50 d-block mb-1 text-uppercase">
                      Purchase History
                    </small>
                    <span className="font-size-md text-black mt-1">
                     You have booked {appointmentCount} appointments.
                    </span>
                  </div>
                  <div className="ml-auto">
                  <div className="bg-midnight-bloom text-center d-flex align-items-center justify-content-center text-white font-size-xl d-50 rounded-circle">
                    <FontAwesomeIcon
                      icon= "money-check-alt"
                      className="font-size-xxl text-white"
                    />
                    </div>
                  </div>
                </div>

              </CardContent>
            </Card>

            <div className="divider opacity-2 my-4" />
            
            <Grid container spacing={4} className="font-size-xs">
              <Grid item xs={6}>
                <Card className="text-center my-2 p-3">
                  <div>
                    <FontAwesomeIcon
                      icon={['far', 'credit-card']}
                      className="font-size-xxl text-success"
                    />
                  </div>
                  <div className="mt-2 line-height-sm">
                    <span className="text-black-50 d-block">Payment Info</span>
                  </div>
                </Card>
              </Grid>
              <Grid item xs={6}>
                <Card className="text-center my-2 p-3">
                  <div>
                    <FontAwesomeIcon
                      icon= "user-cog"
                      className="font-size-xxl text-black"
                    />

                  </div>
                  <div className="mt-2 line-height-sm">
                    <span className="text-black-50 d-block">User Settings</span>
                  </div>
                </Card>
              </Grid>
            </Grid>
          </div>
        </Grid>
        <Grid item xs={12} lg={8}>
          <UserAppointmentCal
            classes  = {props}
          />
          <br></br>
          <UserPurchaseHistory/>
        </Grid>
      </Grid>
      <div className="sidebar-inner-layout-overlay" />
    </Fragment>
  );
};
UserDashboard.propTypes = {
    history: PropTypes.shape({
      push: PropTypes.func,
    }).isRequired,
    match: PropTypes.shape({
      params: PropTypes.shape({
        id: PropTypes.number,
      }),
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

export default UserDashboard;
