import axios from "axios";
import {
  onGlobalSuccess,
  onGlobalError,
  API_HOST_PREFIX,
} from "./serviceHelpers";
import debug from "sabio-debug";
const _logger = debug.extend("userService");
const endpoint = `${API_HOST_PREFIX}/api/users`;
const providerEndpoint = `${API_HOST_PREFIX}/api/tempproviders`;

const addUser = (payload) => {
  _logger("register api endpoint is firing");
  const config = {
    method: "POST",
    url: `${endpoint}/register`,
    data: payload,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const addProvider = (payload) => {
  _logger("registerProvider api endpoint is firing");
  const config = {
    method: "POST",
    url: `${providerEndpoint}/register`,
    data: payload,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const confirmUser = (token) => {
  _logger("confirm registration api endpoint is firing");

  const config = {
    method: "GET",
    url: `${endpoint}/confirm/${token}`,

    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const login = (payload) => {
  _logger("...login is executing...");
  const config = {
    method: "POST",
    url: `${endpoint}/login`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };

  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const facebookAuth = (payload) => {
  _logger("...facebook login is executing...");
  const config = {
    method: "POST",
    url: `${endpoint}/facebookAuth`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const googleAuth = (payload) => {
  _logger("...google login is executing...");
  const config = {
    method: "POST",
    url: `${endpoint}/googleAuth`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getCurrentUser = () => {
  _logger("get user");

  const config = {
    method: "GET",
    url: `${endpoint}/profile`,

    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const logout = () => {
  _logger("get user");

  const config = {
    method: "GET",
    url: `${endpoint}/logout`,

    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAllUser = () => {

  const config = {
    method: "GET",
    url: `${endpoint}`,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const forgotPassword = (payload) => {
  _logger("...forgotPassword is executing...");
  const config = {
    method: "POST",
    data:  payload,
    url: `${endpoint}/forgotpassword`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const passwordUpdate = (payload, token) => {
  _logger("...forgotPassword is executing...");
  const config = {
    method: "PUT",
    url: `${endpoint}/passwordUpdate/${token}`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

export default {
  addUser,
  addProvider,
  confirmUser,
  login,
  getCurrentUser,
  facebookAuth,
  googleAuth,
  logout,
  getAllUser,
  forgotPassword,
  passwordUpdate

};
