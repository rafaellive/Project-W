import React, { Fragment } from "react";
import { Grid, Button, Card } from "@material-ui/core";
import debug from "sabio-debug";
import PropTypes from "prop-types";
import { withRouter } from "react-router-dom";

const _logger = debug.extend("UserProfile");

const UserProfileCard = (props) => {
  _logger(props);

  const toUserDetails = () => {
    props.history.push(`/users/profiles/${props.profile.id}/details`, {
      type: "USER_PROFILE_VIEW",
      userProfile: props.profile,
    });
  };

  _logger("rendering");
  return (
    <Fragment>
        <Grid item xs={8} md={4} lg={3} spacing={3}>
          <Card className="mb-4 card-box" style={{ margin: "20px" }}>
            <div className="card-img-wrapper" style={{ resizeMode: "contain" }}>
              <div className="card-badges card-badges-bottom"></div>
              <img
                src={
                  props.profile.avatarUrl
                    ? props.profile.avatarUrl
                    : "https://st2.depositphotos.com/1009634/7235/v/950/depositphotos_72350117-stock-illustration-no-user-profile-picture-hand.jpg"
                }
                className="card-img-top rounded"
                alt="..."
                style={{
                  objectFit: "cover",
                  width: "100%",
                  height: "300px",
                }}
              />
            </div>
            <div className="card-body text-center">
              <h5 className="card-title font-weight-bold font-size-lg">
                {`${props.profile.firstName} ${
                  props.profile.mi ? props.profile.mi : props.profile.mi
                } ${props.profile.lastName}`}
              </h5>
              <Button
                size="small"
                variant="contained"
                color="primary"
                style={{ color: "#fff" }}
                className="mt-1"
                onClick={toUserDetails}
              >
                Details
              </Button>
            </div>
          </Card>
        </Grid>     
    </Fragment>
  );
};

UserProfileCard.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func,
  }).isRequired,
  profile: PropTypes.shape({
    userId: PropTypes.number,
    id: PropTypes.number,
    firstName: PropTypes.string.isRequired,
    mi: PropTypes.string,
    lastName: PropTypes.string.isRequired,
    avatarUrl: PropTypes.string,
  }).isRequired,
};

export default withRouter(UserProfileCard);
