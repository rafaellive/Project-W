import React from "react";

import PropTypes from "prop-types";

import { Grid, Card, Button } from "@material-ui/core";

const ProviderService = (props) => {
  const oneProviderService = props.providerService;
  if (
    !oneProviderService.medicalService.cpt4Code ||
    oneProviderService.medicalService.cpt4Code === ""
  ) {
    oneProviderService.medicalService.cpt4Code = "No cpt4code";
  }
  if (props.providerService) {
    return (
      <Grid item xs={8} md={3} lg={3} xl={3}>
        <Card
          className="card-box bg-neutral-first border-2 card-border-top border-first text-center mb-4 "
          style={{ minHeight: "280px" }}
        >
          <div className="py-5 px-5">
            <h4 className="font-size-md font-weight-bold mb-2">
              {oneProviderService.medicalService.name}
            </h4>
            <h2 className="font-size-sm mb-2">
              {oneProviderService.medicalServiceType.name}
            </h2>
            <p className="opacity-6 font-size-md mb-3">
              {oneProviderService.medicalService.cpt4Code}
            </p>
            <p className="font-size-sm mb-3">${oneProviderService.price}</p>
          </div>
        </Card>
      </Grid>
    );
  }
};

ProviderService.propTypes = {
  providerService: PropTypes.shape({
    id: PropTypes.number.isRequired,
    providerId: PropTypes.number.isRequired,
    price: PropTypes.number.isRequired,
    service: PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
      cpt4Code: PropTypes.string.isRequired,
    }),
    serviceType: PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
    }),
  }).isRequired,
};

export default React.memo(ProviderService);
