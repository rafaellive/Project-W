import axios from "axios";
import {onGlobalSuccess, onGlobalError, API_HOST_PREFIX } from "../serviceHelpers";
import debug from "sabio-debug";
const _logger = debug.extend("userProfileService");

const endpoint = `${API_HOST_PREFIX}/api/users/profiles/`;

const getProfileById = (id) => {
    _logger(id, "getProfileById is firing");
    const config = {
        method:"GET",
        url: `${endpoint}${id}`,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }   
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

let getUserProfiles = (payload) => {
    _logger("getUserProfiles is firing");
    const config = {
        method: "GET",
        url: `${endpoint}paginate`,
        params: payload,
        withCredentials:true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config)
    .then(onGlobalSuccess)
      .catch(onGlobalError)
};

let createUserProfile = (payload) => {
    _logger("createUserProfile is firing");
    const config = {
        method: "POST",
        url: endpoint ,
        data: payload,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config)
    .then(onGlobalSuccess)
      .catch(onGlobalError)
};

let editUserProfile = (payload) => {
    _logger("editUserProfile is firing");
    const config = {
        method: "PUT",
        url: endpoint + 'current',
        data: payload,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config)
    .then(onGlobalSuccess)
      .catch(onGlobalError)
};

let deleteUserProfile = (payload,userId) => {
    _logger("deleteUserProfile is firing");
    const config = {
        method: "PUT",
        url: `${endpoint}/${userId}`,
        data: payload,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config)
    .then(onGlobalSuccess)
      .catch(onGlobalError)
};

let getCurrent = () => {
    _logger("getCurrentProfile is firing");
    const config = {
        method: "GET",
        url: endpoint + 'current',        
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config)
    .then(onGlobalSuccess)
    .catch(onGlobalError)
};

export default { getProfileById, getUserProfiles, createUserProfile, editUserProfile, deleteUserProfile, getCurrent };
