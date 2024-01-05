import { Alert, AlertTitle, Box, Button, Grid, Typography, badgeClasses, tableSortLabelClasses } from '@mui/material'
import React, { useEffect, useState } from 'react'
import LinearProgress from '@mui/material/LinearProgress'
import axios from 'axios'
import Paper from '@mui/material/Paper'
import {styled} from '@mui/material/styles'
import { Navigate, useNavigate,useLocation} from 'react-router-dom';
import LoginCheck from '../components/LoginCheck'
import TaskModalComponent from '../components/TaskModalComponent'
import {ApiBaseUrl} from "../App";



const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

function TasksPage() {
  const [isFetching, SetIsFetching] = useState(false);
  const [tasks,SetTasks] = useState([]);
  const [tasksAvailable,SetTasksAvailable] = useState(false);
  var redir = null;
  if(LoginCheck() === false){
    redir = <Navigate to ="/login"></Navigate>
    localStorage.clear()
  }
  useEffect(()=>{
    const fetchTasks = async ()=>{
      try{
        const response = await axios.get(ApiBaseUrl+"tasks",{
          headers:{
            Authorization:"bearer " + localStorage.getItem("token")
          }
        })
        const t = await response.data;
        console.log(t)
        SetTasks(t)
        SetTasksAvailable(true);
        const r2 = await axios.get(ApiBaseUrl+"tasks/solved/"+localStorage.getItem("id"),{
          headers:{
            Authorization:"bearer " + localStorage.getItem("token")
          }
        })
        const d = await r2.data;
        localStorage.setItem("solved",d);
      }
      catch{
        console.log("contest not started yet")
      }
  }
    SetIsFetching(true)
    fetchTasks();
    SetIsFetching(false)
  },[])
  return (
    <>
    {isFetching ? <LinearProgress sx={{marginTop:"0%"}}/>:null}
    {redir}
    <Box sx={{
      marginLeft:"5%",
      marginRight:"5%"
    }}>
        <Typography variant='h3' sx={{marginTop:"1%"}}>Challenges</Typography> 
        <Box sx={{marginTop:"2%"}}>
          {tasksAvailable ? <></>: <Alert severity='error'><AlertTitle>CONTEST NOT STARTED YET</AlertTitle></Alert>}
          <Grid container spacing={2}>
            {tasks.map((task)=>{
            return (<Grid key={task.id} item md={4}>
              <Item>
                <Typography variant='h5' color='blue' align='left'>{task.name}</Typography>
                <Typography variant='body1' color='black' align='left'>{task.type}</Typography>
                <div>
                  <Typography variant='h5' color='red' align='right'> {task.cost}</Typography>
                  <Typography variant='h5' color='red' align='left'> Solves: {task.solves}</Typography>
                </div>
                <TaskModalComponent id={task.id} name={task.name} cost={task.cost} desc={task.description} type={task.type} attachments={task.attachments.split(";")} remoteEnv={task.deployedEnvironment.split(";")}></TaskModalComponent>
              </Item>
            </Grid>)
            })}
          </Grid>
        </Box>
    </Box>
    </>
  )
}

export default TasksPage