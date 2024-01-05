import React,{useState} from 'react'
import { Box, Typography } from '@mui/material'
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import LoginIcon from '@mui/icons-material/Login';
import axios from 'axios';
import { useNavigate, Navigate } from 'react-router-dom';
import LinearProgress from '@mui/material/LinearProgress'
import {ApiBaseUrl} from "../App";

export default function LoginPage() {
    const navigate = useNavigate();
    const [inputs, setInputs] = useState({
      username:"",
      password:""
    })
    const [isLoading, setLoading]= useState(false);
  

    const handleChange = (e) =>{
      setInputs((prevState)=>({
        ...prevState,
        [e.target.name]: e.target.value
      }))
    }
    const handleButtonClick = async (e) =>{
      e.preventDefault();
      if(inputs.username.length===0 || inputs.password.length===0){
        alert("Username and password required")
      }
      else{
        setLoading(true);
         await axios.post(ApiBaseUrl+"auth/login",{
         login:inputs.username,
         password:inputs.password 
        }).then(function (response){
          console.log(response.data);
          localStorage.clear();
          localStorage.setItem("token",response.data.value.token);
          localStorage.setItem("username",response.data.value.username);
          localStorage.setItem("admin",response.data.value.isAdmin)
          localStorage.setItem("solved",response.data.value.solved)
          localStorage.setItem("id",response.data.value.id)
        navigate("/");

        }).catch(function (error){
          alert("Failed to login");
        });
        setLoading(false);
      }
    }
  
    return (
        <div sx={{marginTop:''}}>
            {isLoading ? <LinearProgress sx={{marginTop:"0%"}}/>:null}
            <Box sx={{marginTop:"2%"}}
            display='flex' 
                flexDirection={'column'} 
                maxWidth={500} 
                alignItems={'center'} 
                justifyContent={'center'}
                margin="auto"
                padding={3}
                borderRadius={5}
                boxShadow={'5px 5px 10px #ccc'}>
                
                <Typography variant='h4'>Login</Typography>
                <TextField name="username" value={inputs.username} required autoComplete margin='normal' id="outlined-required" label="Login" variant="outlined" onChange={handleChange} />
                <TextField name="password" value={inputs.password} required autoComplete type="password" margin='normal' id="outlined-required" label="Password" variant="outlined" onChange={handleChange} />
                <Button variant='contained' onClick={handleButtonClick}>Sign in</Button>
                <br></br>
                <Button onClick={()=>navigate("/reg")}>Registration</Button>
            </Box>
        </div>

    )
}
