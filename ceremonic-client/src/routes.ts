import { LOGIN_ROUTE, REGISTRATION_ROUTE, INDEX_PAGE_ROUTE, 
    ABOUT_US_ROUTE, MY_WEDDING_ROUTE, LOGIN_PROVIDER_ROUTE, 
    REGISTRATION_PROVIDER_ROUTE, ALL_VENDOR_ROUTE,
    VENDOR_ROUTE,
    MY_WEDDING_SURVEY_ROUTE,
    MESSENGER_ROUTE} from "./utils/constRoutes";
import Auth from "./page/Auth";
import IndexPage from "./page/IndexPage";
import AboutUs from "./page/AboutUs";
import MyWedding from "./page/MyWedding";
import AuthProvider from "./page/AuthProvider";
import Vendor from "./page/Vendor";
import MyWeddingSurvey from "./page/MyWeddingSurvey";
import AllVendors from "./page/AllVendors";
import MessengerPage from "./page/MessengerPage";



export const authRoutes = [
    {
        path: MY_WEDDING_ROUTE,
        Component: MyWedding
    },
    {
        path: VENDOR_ROUTE,
        Component: Vendor
    },
    {
        path: MY_WEDDING_SURVEY_ROUTE,
        Component: MyWeddingSurvey
    },
    {
        path: ALL_VENDOR_ROUTE,
        Component: AllVendors
    },
    {
        path: MESSENGER_ROUTE,
        Component: MessengerPage
    },
]

export const publicRoutes = [
    {
        path: LOGIN_ROUTE,
        Component: Auth
    },
    {
        path: REGISTRATION_ROUTE,
        Component: Auth
    },
    {
        path: LOGIN_PROVIDER_ROUTE,
        Component: AuthProvider
    },
    {
        path: REGISTRATION_PROVIDER_ROUTE,
        Component: AuthProvider
    },
    {
        path: INDEX_PAGE_ROUTE,
        Component: IndexPage
    },
    {
        path: ABOUT_US_ROUTE,
        Component: AboutUs
    },
]