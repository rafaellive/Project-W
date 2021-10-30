import { lazy } from "react";
import { layoutsTypes } from "./layouts";

//breaking up routes by "area" or feature will make it easier to manage this file
const featureOne = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/admin/forms"],
    exact: true,
    roles: ["Admin"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/FormsValidation")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/customer/forms"],
    exact: true,
    roles: ["Customer"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/FormsValidation")),
  },
  {
    layout: layoutsTypes.NOSIDEBAR,
    path: ["/admin/forms/nosidebar"],
    exact: true,
    roles: ["Admin"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/FormsValidation")),
  },
  {
    layout: layoutsTypes.NOSIDEBAR,
    path: ["/customer/forms/nosidebar"],
    exact: true,
    roles: ["Customer"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/FormsValidation")),
  },
];

const featureTwo = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/provider/forms/secure"],
    exact: true,
    roles: ["Provider"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/FormsValidation")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/admin/forms/secure"],
    exact: true,
    roles: ["Admin"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/FormsValidation")),
  },
  {
    layout: layoutsTypes.NOSIDEBAR,
    path: ["/provider/forms/nosidebar/secure"],
    exact: true,
    roles: ["Provider"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/FormsValidation")),
  },
  {
    layout: layoutsTypes.NOSIDEBAR,
    path: ["/admin/forms/nosidebar/secure"],
    exact: true,
    roles: ["Admin"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/FormsValidation")),
  },
];

const payments = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/payments/form"],
    exact: true,
    roles: ["Admin", "Customer"],
    isAnonymous: false,
    component: lazy(() => import("../components/payments/PaymentFormPage")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/payments/review"],
    exact: true,
    roles: ["Admin", "Customer"],
    isAnonymous: false,
    component: lazy(() => import("../components/payments/PaymentReviewPage")),
  },
];

const uploader = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/uploads/example"],
    exact: true,
    roles: ["Admin", "Customer"],
    isAnonymous: false,
    component: lazy(() => import("../components/files/FileUploadExample")),
  },
];

const shoppingCart = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/cart/:id"], // hardcoded as 8 for now, but would be current User Id
    exact: true,
    roles: ["Admin", "Customer"],
    isAnonymous: false,
    component: lazy(() => import("../components/payments/ShoppingCartPage")),
  },
];

const providerDetails = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/providers/new"],
    exact: true,
    roles: ["Admin", "Provider"],
    isAnonymous: false,
    component: lazy(() => import("../pages/providers/ProviderWizard")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/provider/providerId:(d)/edit"],
    exact: true,
    roles: ["Admin", "Provider"],
    isAnonymous: false,
    component: lazy(() => import("../pages/providers/ProviderWizard")),
  },
];

const providerServices = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/provider/services"],
    exact: true,
    roles: ["Admin", "Provider"],
    isAnonymous: false,
    component: lazy(() =>
      import("../components/providers/services/ProviderServiceManager")
    ),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/provider/services/add"],
    exact: true,
    roles: ["Admin", "Provider"],
    isAnonymous: false,
    component: lazy(() =>
      import("../components/providers/services/ProviderServiceAddForm")
    ),
  },
];

const providers = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/provider/:id/details"],
    exact: true,
    roles: ["Admin", "Provider", "Customer"],
    isAnonymous: false,
    component: lazy(() => import("../pages/providers/ProviderDetailsPage")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/provider", "/providers"],
    exact: true,
    roles: ["Admin", "Provider", "Customer"],
    isAnonymous: false,
    component: lazy(() => import("../pages/providers/ProviderLandingPage")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/provider/:providerId/dashboard", "/provider/dashboard"],
    exact: true,
    roles: ["Admin", "Provider"],
    isAnonymous: false,
    component: lazy(() => import("../pages/providers/ProviderDashboard")),
  },
];

const schedules = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/schedule"],
    exact: true,
    roles: ["Admin", "Provider"],
    isAnonymous: false,
    component: lazy(() => import("../pages/schedules/ScheduleLandingPage")),
  },
];

const blogRoutes = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/postBlog", "/postBlog/edit"],
    exact: true,
    roles: ["Admin"],
    isAnonymous: false,
    component: lazy(() => import("../pages/blogs/PostBlog")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/blog/Drafts"],
    exact: true,
    roles: ["Admin"], 
    isAnonymous: false,
    component: lazy(() => import("../pages/blogs/BlogDrafts")),
  },
];

const ratingRoutes = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/ratings"],
    exact: true,
    roles: ["Admin", "Provider", "Customer"],
    isAnonymous: false,
    component: lazy(() => import("../pages/ratings/Ratings")),
  },
];

const userProfile = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/users/profiles/new", "/users/profiles/edit"],
    exact: true,
    roles: ["Admin", "Customer", "Provider"],
    isAnonymous: false,
    component: lazy(() => import("../components/userprofiles/UserProfileForm")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/users/profiles"],
    exact: true,
    roles: ["Admin"],
    isAnonymous: false,
    component: lazy(() => import("../components/userprofiles/UserProfiles")),
  },
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/users/profiles/:id/details", "/users/myprofile"],
    exact: true,
    roles: ["Admin", "Customer", "Provider"],
    isAnonymous: false,
    component: lazy(() =>
      import("../components/userprofiles/UserProfileDetails")
    ),
  },
];

const user = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/user/dashboard"],
    exact: true,
    roles: ["Customer"],
    isAnonymous: false,
    component: lazy(() => import("../components/userDash/UserDashboard")),
  },
];

const adminDash = [
  {
    layout: layoutsTypes.SIDEBAR,
    path: ["/dashboard/admin"],
    exact: true,
    roles: ["Admin"], //the roles that this route can be seen by
    isAnonymous: false,
    component: lazy(() => import("../pages/Admin/AdminDash")),
  },
];

var allRoutes = [
  ...featureOne,
  ...featureTwo,
  ...providerDetails,
  ...providerServices,
  ...blogRoutes,
  ...ratingRoutes,
  ...providers,
  ...schedules,
  ...uploader,
  ...payments,
  ...shoppingCart,
  ...userProfile,
  ...adminDash,
  ...user,
];

export default allRoutes;
