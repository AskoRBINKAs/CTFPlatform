import { Box, Typography } from '@mui/material'
import React from 'react'
import LoginCheck from '../components/LoginCheck'
import { Navigate, useNavigate,useLocation} from 'react-router-dom';


function MainPage() {
  var redir = null;
  if(LoginCheck() === false){
    redir = <Navigate to ="/login"></Navigate>
    localStorage.clear()
  }
  return (
    <Box>
      {redir}
        <Typography variant='h5'>Тут будет офигенный логотип или что-то вроде того</Typography> 
    </Box>
  )
}

export default MainPage