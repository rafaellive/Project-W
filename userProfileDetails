import React, { Fragment, useEffect } from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import userProfileService from "../../services/users/userProfileService";
import PerfectScrollbar from "react-perfect-scrollbar";
import { Grid, Box, Typography, Card, Button } from "@material-ui/core";
import debug from "sabio-debug";
import { Users, Layers } from "react-feather";

const _logger = debug.extend("UserProfileDetails");

function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <Typography
      component="div"
      role="tabpanel"
      hidden={value !== index}
      {...other}
    >
      {value === index && <Box p={0}>{children}</Box>}
    </Typography>
  );
}

TabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.isRequired,
  value: PropTypes.isRequired,
};

const UserProfileDetails = (props) => {
  const [value, setValue] = React.useState(null);
  const [profile, setProfile] = React.useState(
    props.location.state && props.location.state.type === "USER_PROFILE_VIEW"
      ? props.location.state.userProfile
      : null
  );

  useEffect(() => {
    if (!profile) {
      userProfileService.getCurrent().then(onGetSuccess).catch(onGetError);
    }
  }, [profile]);

  const onGetSuccess = (response) => {
    _logger(response, "getProfile response");
    setProfile(response.item);
  };

  const onGetError = (error) => {
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
      <PerfectScrollbar>
        <Grid
          container
          spacing={4}
          style={{ paddingTop: 85, paddingLeft: 100, paddingRight: 50 }}
        >
          <Grid item xs={12} lg={4}>
            <Card className="card-box mb-4 pt-4">
              <div className="d-flex align-items-center px-4 mb-3">
                <div className="avatar-icon-wrapper rounded mr-3">
                  <div className="d-block p-0 avatar-icon-wrapper m-0 d-100">
                    <div className="rounded overflow-hidden">
                      <img
                        alt="..."
                        className="img-fluid"
                        onClick={onClickEdit}
                        src={profile ? profile.avatarUrl : defaultAvatarSrc}
                      />
                    </div>
                  </div>
                </div>
                <div className="w-100">
                  <a
                    href="#/"
                    onClick={(e) => e.preventDefault()}
                    className="font-weight-bold font-size-lg"
                    title="..."
                  >
                    {`${profile ? profile.firstName : "User"} ${
                      profile ? profile.lastName : "Name"
                    }`}
                  </a>
                  <span className="text-black-50 d-block pb-1">
                    Welrus User
                  </span>
                  <div className="d-flex align-items-center pt-2">
                    <Button
                      onClick={onClickEdit}
                      variant="contained"
                      size="small"
                      color="primary"
                      className="mr-3"
                    >
                      Edit
                    </Button>
                  </div>
                </div>
              </div>
              <div className="my-3 font-size-sm p-3 mx-4 bg-secondary rounded-sm">
                <div className="d-flex justify-content-between">
                  <span className="font-weight-bold"></span>
                  <span className="text-black-50"></span>
                </div>
              </div>
              <div className="d-flex flex-wrap mb-1 mx-2">
                <div className="w-50 p-3">
                  <Button
                    color="primary"
                    className="btn-gradient text-white bg-vicious-stance text-left px-4 py-3 w-100 rounded-lg"
                  >
                    <div>
                      <Users className="h1 d-block my-2 text-danger" />
                      <div className="font-weight-bold">Payments</div>
                      <div className="font-size-sm mb-1 opacity-8">
                        Payment Methods
                      </div>
                    </div>
                  </Button>
                </div>
                <div className="w-50 p-3">
                  <Button
                    color="primary"
                    className="btn-gradient text-white bg-royal text-left px-4 py-3 w-100 rounded-lg"
                  >
                    <div>
                      <Layers className="h1 d-block my-2 text-white" />
                      <div className="font-weight-bold">Settings</div>
                      <div className="font-size-sm mb-1 opacity-8">
                        User Settings
                      </div>
                    </div>
                  </Button>
                </div>
              </div>
            </Card>
          </Grid>
        </Grid>
      </PerfectScrollbar>
      <div className="sidebar-inner-layout-overlay" />
    </Fragment>
  );
};

UserProfileDetails.propTypes = {
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
export default UserProfileDetails;
