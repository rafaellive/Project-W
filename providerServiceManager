import React from "react";
import ProviderService from "./ProviderService";
import providerServiceService from "@services/providerServiceService";
import providerDetailsService from "@services/providerDetailsService";
import scheduleService from "@services/scheduleService";

import PropTypes from "prop-types";

import { withStyles } from "@material-ui/core/styles";
import Input from "@material-ui/core/Input";

import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import Select from "@material-ui/core/Select";

import { Pagination } from "@material-ui/lab";

import AddIcon from "@material-ui/icons/Add";

import { Container, Fab, Grid } from "@material-ui/core";

import debug from "sabio-debug";
const _logger = debug.extend("ProviderServiceManager");

const useStyles = (theme) => ({
  root: {
    flexGrow: 1,
  },
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
  },
  paper: {
    padding: theme.spacing(1),
    textAlign: "center",
    color: theme.palette.text.secondary,
  },
});

class ProviderServiceManager extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      mappedProviderServices: [],
      page: {
        pageIndex: 0,
        pageSize: 8,
        totalCount: 0,
        totalPages: 0,
      },
      keyword: "",
    };
  }

  componentDidMount() {
    this.setProviderIdAndPopulatePractices();
  }

  setProviderIdAndPopulatePractices = () => {
    providerDetailsService
      .getCurrentProvider(this.state.providerId)
      .then(this.onGetCurrentProviderSuccess)
      .catch(this.onGetCurrentProviderError);
  };

  onGetCurrentProviderSuccess = (response) => {
    const provider = response.item;
    _logger(provider);
    this.setPracticeDropDownOptions(provider.id);
  };

  onGetCurrentProviderError = (errResponse) => {
    _logger({ error: errResponse.config });
  };

  setPracticeDropDownOptions = (providerId) => {
    scheduleService
      .getPracticesWithScheduleIdByProviderId(providerId)
      .then(this.onGetPracticesSuccess)
      .then(this.setStateWithProviderId(providerId))
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

  setStateWithProviderId = (providerId) => {
    this.setState((prevState) => {
      return {
        providerId,
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

  getAllServicesByProviderIdAndScheduleId = (providerId, scheduleId) => {
    _logger(providerId, scheduleId);
    const { pageIndex, pageSize } = this.state.page;
    providerServiceService
      .getAllPaginated(providerId, scheduleId, pageIndex, pageSize)
      .then(this.onGetAllSucess)
      .then(this.setStateWithProviderServices(scheduleId))
      .catch(this.onGetAllError);
  };

  onGetAllSucess = (response) => {
    const providerServices = response.item.pagedItems;

    return {
      providerServices: providerServices || [],
      totalCount: response.item.totalCount,
      totalPages: response.item.totalPages,
    };
  };

  setStateWithProviderServices = (scheduleId) => {
    return (result) => {
      this.setState((prevState) => {
        return {
          scheduleId,
          providerServices: result.providerServices,
          mappedProviderServices: result.providerServices.map(
            this.mapProviderService
          ),
          page: {
            ...prevState.page,
            totalCount: result.totalCount,
            totalPages: result.totalPages,
          },
        };
      });
    };
  };

  onGetAllError = (errResponse) => {
    _logger({ error: errResponse.config });
  };

  mapProviderService = (oneProviderService) => {
    return (
      <React.Fragment key={`ProviderServiceId-${oneProviderService.id}`}>
        <ProviderService
          providerService={oneProviderService}
          providerServiceClassStyles={this.props}
        />
      </React.Fragment>
    );
  };

  search = (keyword) => {
    const { pageIndex, pageSize } = this.state.page;
    const providerId = this.state.providerId;
    const scheduleId = this.state.scheduleId;
    _logger("keyword: ", keyword);
    if (keyword) {
      providerServiceService
        .searchPaginated(providerId, scheduleId, pageIndex, pageSize, keyword)
        .then(this.onGetAllSucess)
        .then(this.setStateWithProviderServices(scheduleId))
        .catch(this.onSearchError);
    } else {
      providerServiceService
        .getAllPaginated(providerId, scheduleId, pageIndex, pageSize)
        .then(this.onGetAllSucess)
        .then(this.setStateWithProviderServices(scheduleId))
        .catch(this.onGetAllError);
    }
  };

  onSearchError = (errResponse) => {
    _logger(errResponse.message);
  };

  keywordChange = (event) => {
    var keyword = event.target.value;
    this.setState(() => {
      return {
        keyword,
      };
    });
    _logger(this.state.keyword);
    this.search(keyword);
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
        this.getAllServicesByProviderIdAndScheduleId(
          this.state.providerId,
          this.state.scheduleId
        );
      }
    );
  };

  handleChange = (e) => {
    this.getAllServicesByProviderIdAndScheduleId(
      this.state.providerId,
      e.target.value
    );
  };

  renderProviderServices = () => {
    if (this.state.mappedProviderServices.length !== 0) {
      return (
        <React.Fragment>{this.state.mappedProviderServices}</React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <h1 justify="center">Please select a practice...</h1>
        </React.Fragment>
      );
    }
  };

  addMedicalService = () => {
    this.props.history.push("/provider/services/add", {
      type: "providerId",
      providerId: this.state.providerId,
    });
  };

  render() {
    const classes = this.props;
    return (
      <React.Fragment>
        <Container>
          <div>
            <Grid container spacing={1}>
              <Grid item xs={4}>
                <FormControl variant="outlined" className={classes.formControl}>
                  <InputLabel htmlFor="outlined-practice-native-simple">
                    Practice
                  </InputLabel>
                  <Select
                    native
                    value={this.state.practice}
                    onChange={this.handleChange}
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
              </Grid>
              <Grid item xs={4}>
                {this.state.page.pageIndex === 0 ? (
                  <Input
                    variant="outlined"
                    margin="dense"
                    fullWidth
                    value={this.state.keyword}
                    onChange={this.keywordChange}
                    placeholder="Search by keyword"
                  />
                ) : (
                  <label></label>
                )}
              </Grid>
              <Grid item xs={4}>
                <Grid container justify="flex-end">
                  <Fab size="small" color="primary" aria-label="add">
                    <AddIcon onClick={this.addMedicalService} />
                  </Fab>
                </Grid>
              </Grid>
            </Grid>
          </div>
          <div className={classes.root}>
            <Grid
              container
              spacing={1}
              alignContent="center"
              alignItems="center"
              wrap="wrap"
            >
              {this.renderProviderServices()}
            </Grid>
          </div>
          <Grid item xs={4}>
            <Pagination
              count={this.state.page.totalPages}
              variant="outlined"
              color="secondary"
              onChange={this.pageChange}
            />
          </Grid>
        </Container>
      </React.Fragment>
    );
  }
}

ProviderServiceManager.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func,
  }).isRequired,
  currentUser: PropTypes.shape({
    id: PropTypes.number,
  }).isRequired,
  root: PropTypes.shape({
    flexGrow: PropTypes.number.isRequired,
  }),
  formControl: PropTypes.shape({
    padding: PropTypes.number.isRequired,
    minWidth: PropTypes.number.isRequired,
  }),
  containerProviderServices: PropTypes.shape({
    paddingTop: PropTypes.string.isRequired,
  }),
  paper: PropTypes.shape({
    padding: PropTypes.number.isRequired,
    textAlign: PropTypes.string.isRequired,
    color: PropTypes.string.isRequired,
  }),
};

export default withStyles(useStyles)(ProviderServiceManager);
