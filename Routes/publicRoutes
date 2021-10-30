import { lazy } from "react";
import { layoutsTypes } from "./layouts";

//breaking up routes by "area" or feature will make it easier to manage this file
const routes = [
  {
    layout: layoutsTypes.PRESENTATION,
    path: ["/"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../pages/HomePage")),
  },
];

const aboutUs = [
  {
    layout: layoutsTypes.PRESENTATION,
    path: ["/aboutus"],
    exact: false,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../pages/AboutUs")),
  },
];
const users = [
  {
    layout: layoutsTypes.PRESENTATION,
    path: ["/confirm/:emailToken"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../components/register/Confirmation")),
  },
  {
    layout: layoutsTypes.PRESENTATION,
    path: ["/register"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../components/register/RegisterForm")),
  },
  {
    layout: layoutsTypes.PRESENTATION,
    path: ["/login"],
    exact: true,
    roles: [], //the roles that this route can be seen anyone logged in
    isAnonymous: true,
    component: lazy(() => import("../pages/login/Login")),
  },
  {
    layout: layoutsTypes.PRESENTATION,
    path: ["/forgot"],
    exact: true,
    roles: [], //the roles that this route can be seen anyone logged in
    isAnonymous: true,
    component: lazy(() => import("../components/register/RecoverPassword")),
  },
  {
    layout: layoutsTypes.PRESENTATION,
    path: ["/provider/demo/request"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../components/providers/ProviderDemoRequestForm")),
  },
];
const comments = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/comments"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../components/comments/Comments")),
  },
];
const addFaq = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/faqs/new"],
    exact: true,
    roles: ["Admin"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/faqs/AddFaq")),
  },
];
const faqDisplay = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/faqs"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../pages/faqs/FaqDisplay")),
  },
];

const blogRoutes = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/blogSection"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../pages/blogs/BlogSection")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/singleBlog/:blogId"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../pages/blogs/SingleBlog")),
  },
];

const rating = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/rating"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../pages/ratings/Ratings")),
  },
];

const errorRoutes = [
  {
    layout: layoutsTypes.MINIMAL,
    path: ["/errors/404"],
    exact: true,
    roles: [], //the roles that this route can be seen anyone logged in
    isAnonymous: true,
    component: lazy(() => import("../pages/errors/404")),
  },
  {
    layout: layoutsTypes.MINIMAL,
    path: ["/confirmation/error/404"],
    exact: true,
    roles: [], //the roles that this route can be seen anyone logged in
    isAnonymous: true,
    component: lazy(() => import("../pages/errors/ConfirmationError")),
  },
];

const locations = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/locations/create"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() => import("../components/locations/LocationsForm")),
  },
  {
    layout: layoutsTypes.PRESENTATION,
    path: ["/list"],
    exact: true,
    roles: [], //the roles that this route can be seen by
    isAnonymous: true,
    component: lazy(() =>
      import("../components/locations/providerListWithMap")
    ),
  },
];

const contactForm = [
  {
    layout: layoutsTypes.MINIMAL,
    path: ["/contactForm"],
    exact: true,
    roles: [],
    isAnonymous: true,
    component: lazy(() => import("../components/contact/ContactForm")),
  },
];

const passwordUpdate = [
  {
    layout: layoutsTypes.MINIMAL,
    path: ["/passwordUpdate/:emailToken"],
    exact: true,
    roles: [],
    isAnonymous: true,
    component: lazy(() => import("../components/register/PasswordUpdate")),
  },
];

var allRoutes = [
  ...routes,
  ...aboutUs,
  ...comments,
  ...errorRoutes,
  ...rating,
  ...locations,
  ...users,
  ...blogRoutes,
  ...addFaq,
  ...faqDisplay,
  ...contactForm,
  ...passwordUpdate,
];

export default allRoutes;
