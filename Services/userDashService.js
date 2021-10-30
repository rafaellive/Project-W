import axios from "axios";
import {
  onGlobalSuccess,
  onGlobalError,
  API_HOST_PREFIX,
} from "../serviceHelpers";
import debug from "sabio-debug";
const _logger = debug.extend("userDashService");

const endpoint = `${API_HOST_PREFIX}/api/userdashboard`;

const getAllAppointments = (userId) => {
  _logger("-----------getByUserId connecting with API-----------");

  const config = {
    method: "GET",
    url: `${endpoint}/${userId}/appointments`,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

export default { getAllAppointments };
