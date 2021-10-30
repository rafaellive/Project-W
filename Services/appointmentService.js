import axios from "axios";
import debug from 'sabio-debug';

import {onGlobalSuccess, onGlobalError, API_HOST_PREFIX} from "./serviceHelpers";

const endpoint = `${API_HOST_PREFIX}/api/appointment`;

const _logger = debug.extend("providerAppointments");

const getAllByProviderId = (providerId) => {
    _logger("...getAllByProviderId is executing...")
    const config = {
        method: "GET",
        url: `${endpoint}/${providerId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" },
    };

    return axios(config);
};

const getLast30DaysByProviderId = (providerId) => {
    _logger("...get30Days is executing...")
    const config = {
        method: "GET",
        url: `${endpoint}/${providerId}/month`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" },
    };

    return axios(config);
};

const getById = (appointmentId) => {
    _logger("...getById is executing...")
    const config = {
        method: "GET",
        url: `${endpoint}/find/${appointmentId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" },
    };

    return axios(config);
};

const add = (payload) => {
    _logger("...add is executing...")
    const config = {
        method: "POST",
        url: endpoint,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const addEmail = (payload) => {
    _logger("...addEmail is executing...")
    const config = {
        method: "POST",
        url: `${API_HOST_PREFIX}/api/email`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};


const edit = (payload, id) => {
    _logger("...edit is executing...")
    const config = {
        method: "PUT",
        url: `${endpoint}/${id}`,
        params: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" },
    };

    return axios(config);
};

const remove = (id) => {
    _logger("...remove is executing...")
    const config = {
        method: "DELETE",
        url: `${endpoint}/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" },
    };

    return axios(config);
};

export default { getAllByProviderId, getById, add, edit, remove, getLast30DaysByProviderId, addEmail }
