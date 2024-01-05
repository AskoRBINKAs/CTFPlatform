import './App.css';
import {Routes, Route} from 'react-router-dom';
import HeaderComponent from './components/HeaderComponent';
import MainPage from './pages/MainPage';
import TasksPage from './pages/TasksPage';
import AdminPage from './pages/AdminPage';
import ScoreboardPage from './pages/ScoreboardPage';
import RulesPage from './pages/RulesPage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import LogoutPage from './pages/LogoutPage';
import {ToastContainer,toast} from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css';

export const ApiBaseUrl = "/api/v1/";

function App() {
  return (
    <>
      <HeaderComponent></HeaderComponent>
      <div className="App">
        <Routes>
          <Route path='/' element={<MainPage/>}></Route>
          <Route path='tasks/' element={<TasksPage/>}></Route>
          <Route path='scoreboard/' element={<ScoreboardPage/>}></Route>
          <Route path='rules/' element={<RulesPage/>}></Route>
          <Route path='admin/' element={<AdminPage/>}></Route>
          <Route path='login/' element={<LoginPage/>}></Route>
          <Route path='logout/' element={<LogoutPage/>}></Route>
          <Route path='reg/' element={<RegisterPage/>}></Route>
        </Routes>
        <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
        />

      </div>
    </>
  );
}

export default App;
