import React, { Fragment } from "react";
import debug from "sabio-debug";
import userProfileService from "../../services/users/userProfileService";
import UserProfileCard from "../userprofiles/UserProfileCard";
import Grid from "@material-ui/core/Grid";
import Snackbar from "@material-ui/core/Snackbar";
import Alert from "../../assets/components/Alert";
import PerfectScrollbar from "react-perfect-scrollbar";
import clsx from "clsx";
import styles from "../../assets/styles/blog.module.css";
import { Pagination } from "@material-ui/lab";
import { WrapperSimple } from "../../layouts";

const _logger = debug.extend("UserProfiles");

class UserProfiles extends React.Component {
  state = {
    mappedProfiles: [],
    open: false,
    page: {
      pageIndex: 0,
      pageSize: 8,
      totalCount: 0,
      totalPages: 0,
    },
  };

  componentDidMount() {
    this.onGetProfiles();
  }

  onGetProfiles = () => {
    const { pageIndex, pageSize } = this.state.page;
    const payload = {
      pageIndex: pageIndex,
      pageSize: pageSize,
    };
    userProfileService
      .getUserProfiles(payload)
      .then(this.onGetProfilesSuccess)
      .catch(this.onGetProfilesError);
  };

  onGetProfilesSuccess = (response) => {
    _logger(response, "onGetProfiles has fired");

    if (response.isSuccessful) {
      const profiles = response.item.pagedItems;
      this.setState((prevState) => {
        return {
          ...prevState,
          page: {
            ...prevState.page,
            totalCount: response.item.totalCount,
            totalPages: response.item.totalPages,
          },
          mappedProfiles: profiles.map(this.mapProfile),
        };        
      });
    }
  };

  setOpen = () => {
    this.setState(() => {
      return {
        open: true,
      };
    });
  };

  onGetProfilesError = (error) => {
    this.setOpen();
    _logger(error);
  };

  mapProfile = (profile) => {
    return (
      <UserProfileCard
        className={clsx("mb-4", styles.blogGrid)}
        key={profile.id}
        profile={profile}
       />
    );
  };
  pageChange = (event, page) => {
    this.setState(
      (prevState) => {
        return {
          ...prevState,
          page: {
            ...prevState.page,
            pageIndex: page - 1,
          },
        };
      },
      () => {
        this.onGetProfiles();
      }
    );
  };

  render() {
    return (
      <Fragment>
        {" "}
        <PerfectScrollbar>
          <WrapperSimple sectionHeading="Profiles">
            <Grid
              container
              spacing={4}
              style={{ paddingLeft: 100, paddingRight: 50 }}
            >
              <Snackbar open={this.state.open} autoHideDuration={6000}>
                <Alert severity="error">Something went wrong ... </Alert>
              </Snackbar>
              {this.state.mappedProfiles}
            </Grid>
            <Pagination
              className={styles.pagination}
              count={this.state.page.totalPages}
              variant="outlined"
              color="secondary"
              onChange={this.pageChange}
            />
          </WrapperSimple>{" "}
        </PerfectScrollbar>
      </Fragment>
    );
  }
}

export default UserProfiles;
