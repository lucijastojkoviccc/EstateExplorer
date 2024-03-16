import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import Projekti from "./components/Projekti";
import { FetchData } from "./components/FetchData";
import Home  from "./components/Home";
import Zgrada from "./components/Zgrada";
import DodajZgradu from './components/DodajZgradu';
import DodajStanove from './components/DodajStanove';
import KupiStan from './components/KupiStan';
import Stan from './components/Stan';
import { RatePrikaz } from './components/Rata/RatePrikaz';
import NovaZgrada from './components/NovaZgrada';
import ONama from './components/ONama';
import SetRole from './components/SetRole';

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/projekti',
        element: <Projekti />
    },
    {
        path: '/fetch-data',
        requireAuth: true,
        element: <FetchData />
    },
    {
        path: '/zgrada/:zgradaid',
        element: <Zgrada />
    },
    {
        path: '/dodajzgradu',
        element: <DodajZgradu />
    },
    {
        path: '/dodajstanove',
        element: <DodajStanove />
    },
    {
        path: '/kupiStan/:zgradaid',
        element: <KupiStan />
    },
    {
        path: '/kupiStan/:zgradaid/:stanid/',
        element: <Stan />
    },
    {
        path: '/rate',
        element: <RatePrikaz />
    },
    {
        path: '/novaZgrada',
        element: <NovaZgrada />
    },
    {
        path: '/ONama',
        element: <ONama />
    },
    {
        path: '/set-role/:uloga',
        element: <SetRole />
    },  
    {
        path: 'Identity/Account/Login',
        Component: () => {
            window.location.href = 'Identity/Account/Login';
            return null;
        }
       
    },
    
    {
        path: 'Identity/Account/Register',
        Component: () => {
            window.location.href = 'Identity/Account/Register';
            return null;
        }

    },
    {
        path: 'Identity/Account/Logout',
        Component: () => {
            window.location.href =  'Identity/Account/Logout';
            return null;
        }

    },
    {
        path: 'Identity/Account/RegisterConfirmation',
        Component: () => {
            window.location.href = 'Identity/Account/RegisterConfirmation';
            return null;
        }

    },
    {
        path: 'Identity/Account/ResendEmailConfirmation',
        Component: () => {
            window.location.href = 'Identity/Account/ResendEmailConfirmation';
            return null;
        }

    },
    {
        path: 'Identity/Account/ResetPassword',
        Component: () => {
            window.location.href = 'Identity/Account/ResetPassword';
            return null;
        }

    },
    {
        path: 'Identity/Account/ResetPasswordConfirmation',
        Component: () => {
            window.location.href = 'Identity/Account/ResetPasswordConfirmation';
            return null;
        }

    },
    {
        path: 'Identity/Account/ForgotPassword',
        Component: () => {
            window.location.href = 'Identity/Account/ForgotPassword';
            return null;
        }

    },
    {
        path: 'Identity/Account/ForgotPasswordConfirmation',
        Component: () => {
            window.location.href = 'Identity/Account/ForgotPasswordConfirmation';
            return null;
        }

    },
    {
        path: 'Identity/Account/AccessDenied',
        Component: () => {
            window.location.href = 'Identity/Account/AccessDenied';
            return null;
        }

    },

 
    ...ApiAuthorzationRoutes
];

export default AppRoutes;
