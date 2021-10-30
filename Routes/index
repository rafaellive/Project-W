import securedRoutes from "./securedRoutes";
import publicRoutes from "./publicRoutes";
import { layoutsTypes as routeTypes } from "./layouts";
import debug from "sabio-debug";

const _logger = debug.extend("routes");

const allRoutes = [...securedRoutes, ...publicRoutes];

export const anonymous = publicRoutes;
export const secured = securedRoutes;
export const layoutsTypes = routeTypes;

const routesByLayout = {};

// capture all the routes by layout so we know where to render
// them inside the main App component

for (let index = 0; index < allRoutes.length; index++) {
  const aRoute = allRoutes[index];

  if (!routesByLayout[aRoute.layout]) {
    routesByLayout[aRoute.layout] = [];
  }

  routesByLayout[aRoute.layout].push(aRoute);
}

_logger({ routesByLayout });

var routesDir = {
  layoutsTypes,
  securedRoutes,
  publicRoutes,
  allRoutes,
  byLayout: routesByLayout,
};

export default routesDir;
