import { Box, Typography } from '@mui/material'
import React, { useEffect, useState } from 'react'
import { Navigate, useNavigate,useLocation} from 'react-router-dom';
import LoginCheck from '../components/LoginCheck'
import axios from 'axios';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import {ApiBaseUrl} from "../App";

//var apiUrl = "https://localhost:7107/api/";

function compare( a, b ) {
  if ( a.score < b.score ){
    return 1;
  }
  if ( a.score > b.score ){
    return -1;
  }
  return 0;
}

function ScoreboardPage() {
  var redir = null;
  if(LoginCheck() === false){
    redir = <Navigate to ="/login"></Navigate>
    localStorage.clear()
  }

  const [participiants,SetParticipiants] = useState([])
  useEffect(()=>{
    const fetchScores = async ()=>{
      try{
        const response = await axios.get(ApiBaseUrl+"users",{
          headers:{
            Authorization: "bearer " + localStorage.getItem("token")
          }
        })
        const t = await response.data;
        console.log(t)
        t.sort(compare);
        SetParticipiants(t);
      }
      catch (error){
        alert("Scoreboard is unavailable")
      }
    }
    fetchScores()
  },[])
  var place = 0;
  return (
    <Box>
      {redir}
        <Typography variant='h3' sx={{marginTop:"1%"}}>Scoreboard</Typography> 
        <Box sx={{
      marginLeft:"5%",
      marginRight:"5%",
      marginTop:"2%"
    }}>
          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell align="center">Place</TableCell>
                  <TableCell align="center">Name</TableCell>
                  <TableCell align="center">Score</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {participiants.map((t) => {
                  place++;
                  return(<TableRow
                    key={t.id}
                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                  >

                    <TableCell align="center">{place}</TableCell>
                    <TableCell align="center">{t.name}</TableCell>
                    <TableCell align="center">{t.score}</TableCell>
                  </TableRow>)
                })}
              </TableBody>
            </Table>
          </TableContainer>
        </Box>
    </Box>
  )
}

export default ScoreboardPage