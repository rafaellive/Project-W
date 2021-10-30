import React, { Fragment } from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import PerfectScrollbar from "react-perfect-scrollbar";
import { Grid, Card, List, ListItem, Button, ButtonGroup } from "@material-ui/core";
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider,
} from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import CountUp from "react-countup";
import Circle from "react-circle";
import appointmentService from "@services/appointmentService";
import providerServiceService from "@services/providerServiceService";
import debug from "sabio-debug";
import "../../../assets/styles/providerDashboard.css";
const _logger = debug.extend("AppointmentAnalytics");

class AppointmentAnalytics extends React.Component {
  state = {
    startDate: ["02/01/2021"],
    endDate: ["02/28/2021"],
    monthlyAppointments: {},
    topServices: {
      topOneService: {
        id: 0,
        name: "",
        cpt4Code: "",
        totalCount: 0,
        price: 0,
      },
      topTwoService: {
        id: 0,
        name: "",
        cpt4Code: "",
        totalCount: 0,
        price: 0,
      },
      topThreeService: {
        id: 0,
        name: "",
        cpt4Code: "",
        totalCount: 0,
        price: 0,
      },
      topFourService: {
        id: 0,
        name: "",
        cpt4Code: "",
        totalCount: 0,
        price: 0,
      },
      topFiveService: {
        id: 0,
        name: "",
        cpt4Code: "",
        totalCount: 0,
        price: 0,
      },
    },
    totalRevenue: 0,
  };

  componentDidUpdate(prevProps, prevState) {
    let id = this.props.providerId;

    if (id !== prevProps.providerId) {
      this.getLast30Days();
      this.getTopServices();
    }
    if (
      prevState.topServices.topOneService !==
        this.state.topServices.topOneService ||
      prevState.topServices.topTwoService !==
        this.state.topServices.topTwoService ||
      prevState.topServices.topThreeService !==
        this.state.topServices.topThreeService ||
      prevState.topServices.topFourService !==
        this.state.topServices.topFourService ||
      prevState.topServices.topFiveService !==
        this.state.topServices.topFiveService
    ) {
      this.calculateTotalRevenue();
    }
    _logger("analytics component finished updating...........");
  }

  getLast30Days = () => {
    let id = this.props.providerId;

    if (id) {
      appointmentService
        .getLast30DaysByProviderId(id)
        .then(this.onGetLast30DaysSuccess)
        .catch(this.onGetLast30DaysError);
    }
  };
  onGetLast30DaysSuccess = (response) => {
    let appointments = response.data.items;

    _logger({ success: appointments });
    if (appointments) {
      this.setState((prevState) => {
        let monthlyAppointments = { ...prevState.monthlyAppointments };
        monthlyAppointments = appointments;
        return { monthlyAppointments };
      });
    }
  };

  onGetLast30DaysError = (error) => {
    _logger({ error: error });
  };

  getTopServices = () => {
    let id = this.props.providerId;

    if (id) {
      providerServiceService
        .getTopServices(id)
        .then(this.onGetTopServicesSuccess)
        .catch(this.onGetTopServicesError);
    }
  };
  onGetTopServicesSuccess = (response) => {
    let services = response.data.items;
    _logger({ success: services });
    let emptyService = {
      id: 0,
      name: "No service found",
      cpt4Code: "...",
      totalCount: 0,
      price: 0,
    };

    let oneService = response.data.items[0];
    let twoService = response.data.items[1];
    let threeService = response.data.items[2];
    let fourService = response.data.items[3];
    let fiveService = response.data.items[4];

    if (services.length === 5) {
      this.setState((prevState) => {
        let topServices = { ...prevState.topServices };
        topServices.topOneService = oneService;
        topServices.topTwoService = twoService;
        topServices.topThreeService = threeService;
        topServices.topFourService = fourService;
        topServices.topFiveService = fiveService;

        return { topServices };
      }, this.calculateTotalRevenue());
    }
    if (services.length === 4) {
      this.setState((prevState) => {
        let topServices = { ...prevState.topServices };
        topServices.topOneService = oneService;
        topServices.topTwoService = twoService;
        topServices.topThreeService = threeService;
        topServices.topFourService = fourService;
        topServices.topFiveService = emptyService;

        return { topServices };
      }, this.calculateTotalRevenue());
    }
    if (services.length === 3) {
      this.setState((prevState) => {
        let topServices = { ...prevState.topServices };
        topServices.topOneService = oneService;
        topServices.topTwoService = twoService;
        topServices.topThreeService = threeService;
        topServices.topFourService = emptyService;
        topServices.topFiveService = emptyService;

        return { topServices };
      }, this.calculateTotalRevenue());
    }

    if (services.length === 2) {
      this.setState((prevState) => {
        let topServices = { ...prevState.topServices };
        topServices.topOneService = oneService;
        topServices.topTwoService = twoService;
        topServices.topThreeService = emptyService;
        topServices.topFourService = emptyService;
        topServices.topFiveService = emptyService;

        return { topServices };
      }, this.calculateTotalRevenue());

      if (services.length === 1) {
        this.setState((prevState) => {
          let topServices = { ...prevState.topServices };
          topServices.topOneService = oneService;
          topServices.topTwoService = emptyService;
          topServices.topThreeService = emptyService;
          topServices.topFourService = emptyService;
          topServices.topFiveService = emptyService;

          return { topServices };
        }, this.calculateTotalRevenue());
      }
    }
  };

  onGetTopServicesError = (error) => {
    _logger({ error: error });
  };

  handleStartDateChange = (startDate) => {
    let formatedDate = startDate.toLocaleString();
    formatedDate = formatedDate.split(",", 1);

    if (startDate !== null) {
      this.setState((prevState) => {
        let startDate = { ...prevState.startDate };
        startDate = formatedDate;

        return { startDate };
      });
    }
  };

  handleEndDateChange = (endDate) => {
    let formatedDate = endDate.toLocaleString();
    formatedDate = formatedDate.split(",", 1);

    if (endDate !== null) {
      this.setState((prevState) => {
        let endDate = { ...prevState.endDate };
        endDate = formatedDate;

        return { endDate };
      });
    }
  };
  handleSubmitDates = () => {
    _logger("submit dates firing......");
    let id = this.props.providerId;
    let start = this.state.startDate;
    let end = this.state.endDate;

    if (id & start || end !== null) {
      providerServiceService
        .getTopServicesByDate(id, start, end)
        .then(this.onGetTopServicesSuccess)
        .catch(this.onGetTopServicesByDateError);
    }
  };
  handleToday = () => {
    _logger("getting todays analytics....");
    let id = this.props.providerId;
    let today = new Date();
    let start = (today.getMonth() + 1) + '/' + (today.getDate() - 1) + '/' + today.getFullYear();
    let end = (today.getMonth() + 1) + '/' + (today.getDate() + 1) + '/' + today.getFullYear();
    _logger(start, end)
    if (id & start || end !== null) {
      providerServiceService
        .getTopServicesByDate(id, start, end)
        .then(this.onGetTopServicesSuccess)
        .catch(this.onGetTopServicesByDateError);
    }
  }
  handleWeek = () => {
    _logger("getting this week's analytics....");
    let id = this.props.providerId;
    let curr = new Date;
    let start = new Date(curr.setDate(curr.getDate() - curr.getDay()));
    let end = new Date(curr.setDate(curr.getDate() - curr.getDay()+6));
    let formatedStart = start.toLocaleString();
    formatedStart = formatedStart.split(",", 1);
    let formatedEnd = end.toLocaleString();
    formatedEnd = formatedEnd.split(",", 1);
    start = formatedStart;
    end = formatedEnd;
    _logger(start, end)
    if (id & start || end !== null) {
      providerServiceService
        .getTopServicesByDate(id, start, end)
        .then(this.onGetTopServicesSuccess)
        .catch(this.onGetTopServicesByDateError);
    }
  }
  handleMonth = () => {
    _logger("getting this months's analytics....");
    let id = this.props.providerId;
    let date = new Date(), y = date.getFullYear(), m = date.getMonth();
    let start = new Date(y, m, 1);
    let end = new Date(y, m + 1, 0);
    let formatedStart = start.toLocaleString();
    formatedStart = formatedStart.split(",", 1);
    let formatedEnd = end.toLocaleString();
    formatedEnd = formatedEnd.split(",", 1);
    start = formatedStart;
    end = formatedEnd;
    _logger(start, end)
    if (id & start || end !== null) {
      providerServiceService
        .getTopServicesByDate(id, start, end)
        .then(this.onGetTopServicesSuccess)
        .catch(this.onGetTopServicesByDateError);
    }
  }

  onGetTopServicesByDateError = (error) => {
    _logger({ error: error });
  };
  calculateTotalRevenue = () => {
    let revenue =
      this.state.topServices.topOneService.totalCount *
        this.state.topServices.topOneService.price +
      this.state.topServices.topTwoService.totalCount *
        this.state.topServices.topTwoService.price +
      this.state.topServices.topThreeService.totalCount *
        this.state.topServices.topThreeService.price +
      this.state.topServices.topFourService.totalCount *
        this.state.topServices.topFourService.price +
      this.state.topServices.topFiveService.totalCount *
        this.state.topServices.topFiveService.price;

    if (revenue !== 0) {
      this.setState((prevState) => {
        let totalRevenue = { ...prevState.totalRevenue };
        totalRevenue = revenue;
        return { totalRevenue };
      });
    }
  };

  render() {
    return (
      <Fragment>
        <Grid
          container
          spacing={4}
          className="width-for-laptop width-for-laptop-l width-for-mobile"
        >
          <Grid item xs={12} md={6} lg={12}>
            <Card className="card-box mb-4" style={{ height: "802px" }}>
              <div className="card-header pr-2">
                <div
                  className="card-header--title py-2 font-size-lg font-weight-bold"
                  style={{ textAlignLast: "center" }}
                >
                  Appointment Analytics
                </div>
              </div>

              <div className="px-4 pt-0 pb-3">
                <div
                  className="d-flex align-items-center pt-3 font-weight-bold center-for-laptop center-for-mobile
                  remove-margin-for-mobile remove-margin-for-laptop  remove-margin-for-tablet"
                  style={({ placeContent: "center" }, { marginLeft: "165px" })}
                >
                  <Grid
                    item
                    xs={12}
                    md={6}
                    lg={10}
                    className="display-block-mobile  display-block-laptop "
                  >
                    <div>
                      <ButtonGroup
                        style={{paddingLeft: "50px"}}
                      > 
                        <Button
                        onClick={this.handleToday}  
                        className="m-2 text-white btn-gradient bg-night-sky"
                        color="inherit">TODAY</Button>
                        <Button
                        onClick={this.handleWeek} 
                        className="m-2 text-white btn-gradient bg-night-sky"
                        color="inherit">WEEK</Button>
                        <Button
                        onClick={this.handleMonth} 
                        className="m-2 text-white btn-gradient bg-night-sky"
                        color="inherit">MONTH</Button>
                      </ButtonGroup>
                    </div>
                    <div
                      className="card-header--title font-weight-bold text-uppercase font-size-md text-black-50
                      remove-for-mobile"
                      style={{ 
                        textAlign: "center",
                        paddingRight: "80px"
                      }}
                    >
                      Select Date Range

                      <Button
                      onClick={this.handleSubmitDates}
                      style={{
                        display: "contents"}}
                      color="primary"
                      autoFocus
                      className="p-0 mb-0 font-for-mobile-l 
                      remove-margin-for-mobile remove-margin-for-laptop "
                    >
                      <div
                        className="d-none show-for-mobile show-for-laptop  margin-for-button-laptop-l display-for-button display-for-button-laptop-l 
                        margin-left-for-mobile-button padding-for-laptop-button  display-for-mobile-button
                      remove-padding-for-laptop remove-padding-for-mobile"
                      >
                        Submit
                      </div>
                      <FontAwesomeIcon
                        icon={["fas", "sync-alt"]}
                        className="text-black mr-0 font-size-md  margin-for-button
                        remove-for-mobile remove-for-laptop"
                      />
                    </Button>
                    </div>
                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                      <KeyboardDatePicker
                        disableToolbar
                        variant="inline"
                        format="MM/dd/yyyy"
                        margin="normal"
                        value={this.state.startDate}
                        onChange={this.handleStartDateChange}
                        KeyboardButtonProps={{
                          "aria-label": "change date",
                        }}
                        style={{ paddingRight: "5px" }}
                        className="remove-padding-for-mobile remove-padding-for-laptop "
                      />
                    </MuiPickersUtilsProvider>

                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                      <KeyboardDatePicker
                        disableToolbar
                        variant="inline"
                        format="MM/dd/yyyy"
                        margin="normal"
                        value={this.state.endDate}
                        onChange={this.handleEndDateChange}
                        KeyboardButtonProps={{
                          "aria-label": "change date",
                        }}
                        style={{ paddingLeft: "5px" }}
                        className="remove-padding-for-mobile remove-padding-for-laptop"
                      />
                    </MuiPickersUtilsProvider>

                  </Grid>
                </div>
              </div>
              <div
                className="text-uppercase px-3 pt-3 pb-1 text-black-50 
                remove-margin-for-mobile remove-margin-for-laptop"
                style={{ marginBottom: "15px" }}
              >
                Top Services
              </div>
              <div
                className="scroll-area shadow-overflow"
                style={{ height: "fit-content" }}
              >
                <PerfectScrollbar>
                  <List>
                    <ListItem
                      className="d-flex justify-content-between align-items-center py-2 
                      remove-margin-for-mobile remove-margin-for-laptop"
                      style={{ marginBottom: "10px" }}
                    >
                      <div className="d-flex align-items-center ">
                        <Circle
                          animate={true}
                          animationDuration="3s"
                          responsive={false}
                          size={64}
                          lineWidth={24}
                          progress={
                            this.state.topServices.topOneService.totalCount
                              ? this.state.topServices.topOneService.totalCount
                              : 0
                          }
                          progressColor="#66DA26"
                          bgColor="#edeef1"
                          textColor="#3b3e66"
                          textStyle={{
                            fontSize: "60px",
                            fontWeight: "bold",
                          }}
                          roundedStroke={true}
                          showPercentage={true}
                          showPercentageSymbol={false}
                        />
                        <div className="pb-1 pl-2 text-black font-for-mobile">
                          {this.state.topServices.topOneService.name
                            ? this.state.topServices.topOneService.name
                            : "..."}
                        </div>
                      </div>
                      <div className="text-black d-flex align-items-center">
                        <small className="opacity-6 pr-1">$</small>
                        <span>
                          <CountUp
                            start={0}
                            end={
                              this.state.topServices.topOneService.totalCount &&
                              this.state.topServices.topOneService.price
                                ? this.state.topServices.topOneService
                                    .totalCount *
                                  this.state.topServices.topOneService.price
                                : 0
                            }
                            duration={4}
                            separator=""
                            decimals={0}
                            decimal=","
                            prefix=""
                            suffix=""
                          />
                        </span>
                      </div>
                    </ListItem>
                    <ListItem
                      className="d-flex justify-content-between align-items-center py-2 
                      remove-margin-for-mobile remove-margin-for-laptop"
                      style={{ marginBottom: "10px" }}
                    >
                      <div className="d-flex align-items-center">
                        <Circle
                          animate={true}
                          animationDuration="3s"
                          responsive={false}
                          size={64}
                          lineWidth={24}
                          progress={
                            this.state.topServices.topTwoService.totalCount
                              ? this.state.topServices.topTwoService.totalCount
                              : 0
                          }
                          progressColor="#2E93fA"
                          bgColor="#edeef1"
                          textColor="#3b3e66"
                          textStyle={{
                            fontSize: "60px",
                            fontWeight: "bold",
                          }}
                          roundedStroke={true}
                          showPercentage={true}
                          showPercentageSymbol={false}
                        />
                        <div className="pb-1 pl-2 text-black font-for-mobile">
                          {this.state.topServices.topTwoService.name
                            ? this.state.topServices.topTwoService.name
                            : "..."}
                        </div>
                      </div>
                      <div className="text-black d-flex align-items-center">
                        <small className="opacity-6 pr-1">$</small>
                        <span>
                          <CountUp
                            start={0}
                            end={
                              this.state.topServices.topTwoService.totalCount &&
                              this.state.topServices.topTwoService.price
                                ? this.state.topServices.topTwoService
                                    .totalCount *
                                  this.state.topServices.topTwoService.price
                                : 0
                            }
                            duration={4}
                            separator=""
                            decimals={0}
                            decimal=","
                            prefix=""
                            suffix=""
                          />
                        </span>
                      </div>
                    </ListItem>
                    <ListItem
                      className="d-flex justify-content-between align-items-center py-2 
                      remove-margin-for-mobile remove-margin-for-laptop"
                      style={{ marginBottom: "10px" }}
                    >
                      <div className="d-flex align-items-center">
                        <Circle
                          animate={true} // Boolean: Animated/Static progress
                          animationDuration="3s" //String: Length of animation
                          responsive={false} // Boolean: Make SVG adapt to parent size
                          size={64} // Number: Defines the size of the circle.
                          lineWidth={24} // Number: Defines the thickness of the circle's stroke.
                          progress={
                            this.state.topServices.topThreeService.totalCount
                              ? this.state.topServices.topThreeService
                                  .totalCount
                              : 0
                          } // Number: Update to change the progress and percentage.
                          progressColor="#E91E63" // String: Color of "progress" portion of circle.
                          bgColor="#edeef1" // String: Color of "empty" portion of circle.
                          textColor="#3b3e66" // String: Color of percentage text color.percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                          textStyle={{
                            fontSize: "60px",
                            fontWeight: "bold",
                          }}
                          roundedStroke={true} // Boolean: Rounded/Flat line ends
                          showPercentage={true} // Boolean: Show/hide percentage.
                          showPercentageSymbol={false} // Boolean: Show/hide only the "%" symbol.
                        />
                        <div className="pb-1 pl-2 text-black font-for-mobile">
                          {this.state.topServices.topThreeService.name
                            ? this.state.topServices.topThreeService.name
                            : "..."}
                        </div>
                      </div>
                      <div className="text-black d-flex align-items-center">
                        <small className="opacity-6 pr-1">$</small>
                        <span>
                          <CountUp
                            start={0}
                            end={
                              this.state.topServices.topThreeService
                                .totalCount &&
                              this.state.topServices.topThreeService.price
                                ? this.state.topServices.topThreeService
                                    .totalCount *
                                  this.state.topServices.topThreeService.price
                                : 0
                            }
                            duration={4}
                            separator=""
                            decimals={0}
                            decimal=","
                            prefix=""
                            suffix=""
                          />
                        </span>
                      </div>
                    </ListItem>
                    <ListItem
                      className="d-flex justify-content-between align-items-center py-2 
                      remove-margin-for-mobile remove-margin-for-laptop"
                      style={{ marginBottom: "10px" }}
                    >
                      <div className="d-flex align-items-center">
                        <Circle
                          animate={true}
                          animationDuration="3s"
                          responsive={false}
                          size={64}
                          lineWidth={24}
                          progress={
                            this.state.topServices.topFourService.totalCount
                              ? this.state.topServices.topFourService.totalCount
                              : 0
                          }
                          progressColor="#66DA26"
                          bgColor="#edeef1"
                          textColor="#3b3e66"
                          textStyle={{
                            fontSize: "60px",
                            fontWeight: "bold",
                          }}
                          roundedStroke={true}
                          showPercentage={true}
                          showPercentageSymbol={false}
                        />
                        <div className="pb-1 pl-2 text-black font-for-mobile">
                          {this.state.topServices.topFourService.name
                            ? this.state.topServices.topFourService.name
                            : "..."}
                        </div>
                      </div>
                      <div className="text-black d-flex align-items-center">
                        <small className="opacity-6 pr-1">$</small>
                        <span>
                          <CountUp
                            start={0}
                            end={
                              this.state.topServices.topFourService
                                .totalCount &&
                              this.state.topServices.topFourService.price
                                ? this.state.topServices.topFourService
                                    .totalCount *
                                  this.state.topServices.topFourService.price
                                : 0
                            }
                            duration={4}
                            separator=""
                            decimals={0}
                            decimal=","
                            prefix=""
                            suffix=""
                          />
                        </span>
                      </div>
                    </ListItem>
                    <ListItem
                      className="d-flex justify-content-between align-items-center py-2 
                      remove-margin-for-mobile remove-margin-for-laptop"
                      style={{ marginBottom: "12px" }}
                    >
                      <div className="d-flex align-items-center">
                        <Circle
                          animate={true}
                          animationDuration="3s"
                          responsive={false}
                          size={64}
                          lineWidth={24}
                          progress={
                            this.state.topServices.topFiveService.totalCount
                              ? this.state.topServices.topFiveService.totalCount
                              : 0
                          }
                          progressColor="#2E93fA"
                          bgColor="#edeef1"
                          textColor="#3b3e66"
                          textStyle={{
                            fontSize: "60px",
                            fontWeight: "bold",
                          }}
                          roundedStroke={true}
                          showPercentage={true}
                          showPercentageSymbol={false}
                        />
                        <div className="pb-1 pl-2 text-black font-for-mobile">
                          {this.state.topServices.topFiveService.name
                            ? this.state.topServices.topFiveService.name
                            : "..."}
                        </div>
                      </div>
                      <div className="text-black d-flex align-items-center">
                        <small className="opacity-6 pr-1">$</small>
                        <span>
                          <CountUp
                            start={0}
                            end={
                              this.state.topServices.topFiveService
                                .totalCount &&
                              this.state.topServices.topFiveService.price
                                ? this.state.topServices.topFiveService
                                    .totalCount *
                                  this.state.topServices.topFiveService.price
                                : 0
                            }
                            duration={4}
                            separator=""
                            decimals={0}
                            decimal=","
                            prefix=""
                            suffix=""
                          />
                        </span>
                      </div>
                    </ListItem>
                    <ListItem
                      className="d-flex justify-content-between align-items-center py-2 
                      remove-margin-for-mobile remove-margin-for-laptop"
                      style={{ marginBottom: "10px" }}
                    >
                      <div className="d-flex align-items-center">
                        <div className="text-uppercase px-1 pt-2 pb-1 text-black-50">
                          Total Revenue{" "}
                        </div>
                      </div>
                      <div className="text-black d-flex align-items-center ">
                        <small className="opacity-6 pr-1">$</small>
                        <span>
                          <CountUp
                            start={0}
                            end={
                              this.state.totalRevenue
                                ? this.state.totalRevenue
                                : 0
                            }
                            duration={4}
                            separator=""
                            decimals={0}
                            decimal=","
                            prefix=""
                            suffix=""
                          />
                        </span>
                      </div>
                    </ListItem>
                    <ListItem
                      className="d-flex justify-content-between align-items-center py-2
                     remove-margin-for-mobile remove-margin-for-laptop"
                    >
                      <div className="d-flex align-items-center">
                        <div className="text-uppercase px-1 pt-2 pb-1 text-black-50">
                          Total Created Last 30 Days{" "}
                        </div>
                      </div>
                      <div className="text-black-50 d-flex align-items-center">
                        <small className="text-black pr-1 font-size-md">
                          {this.state.monthlyAppointments
                            ? this.state.monthlyAppointments.length
                            : 0}
                        </small>
                      </div>
                    </ListItem>
                  </List>
                </PerfectScrollbar>
              </div>
            </Card>
          </Grid>
        </Grid>
      </Fragment>
    );
  }
}
AppointmentAnalytics.propTypes = {
  providerId: PropTypes.number.isRequired,
};
export default AppointmentAnalytics;
