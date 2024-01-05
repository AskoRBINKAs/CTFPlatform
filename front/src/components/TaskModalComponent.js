import React, { useState } from 'react'
import { Button, TextField } from '@mui/material'
import { styled } from '@mui/material/styles';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import Typography from '@mui/material/Typography';
import axios from 'axios';
import {toast} from 'react-toastify'
import {ApiBaseUrl} from "../App";


const BootstrapDialog = styled(Dialog)(({ theme }) => ({
    '& .MuiDialogContent-root': {
      padding: theme.spacing(2),
    },
    '& .MuiDialogActions-root': {
      padding: theme.spacing(1),
    },
  }));

export default function TaskModalComponent ({id,name,desc,attachments,remoteEnv}){
    const [open, setOpen] = React.useState(false);
    const [toasted,SetToasted] = useState(false)
    const successToast = () => toast.success('Flag accepted!', {
      position: "top-right",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
      });
    const wrongFlagToast = () => toast.error('Wrong flag!', {
      position: "top-right",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
      });
    const [inputs,setInputs] = useState({
        flag:""
    })
    const handleClickOpen = () => {
      setOpen(true);
    };
    const handleClose = () => {
      setOpen(false);
    };

    var solved = false;
    for(let t in localStorage.getItem("solved").split(';')){
      if (t.toString() === id.toString()) solved = true;
    }
    const handleSubmit = async () =>{
        try{
            const response = await axios.post(ApiBaseUrl+"tasks/submit/"+id,{},{
                headers:{
                    Authorization : "bearer " + localStorage.getItem("token")
                },
                params:{
                    flag:inputs.flag
                }
            });
            toast(successToast);

        }catch (error){
            console.log(error.response.data)
            if(!toasted){
              SetToasted(true)
              toast(wrongFlagToast)
            }
        }
    };
    const handleChange = (e) =>{
        setInputs((prevState)=>({
          ...prevState,
          [e.target.name]: e.target.value
        }))
      }
  console.log(attachments)
  return (
    <>
        <Button variant='contained' fullWidth color='success' onClick={handleClickOpen}>Open / Submit</Button>
        <BootstrapDialog
        onClose={handleClose}
        aria-labelledby="customized-dialog-title"
        open={open}
        fullWidth>
        <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
          <Typography variant='h5'>{name}</Typography>
        </DialogTitle>
        <IconButton
          aria-label="close"
          onClick={handleClose}
          sx={{
            position: 'absolute',
            right: 8,
            top: 8,
            color: (theme) => theme.palette.grey[500],
          }}
        >
          <CloseIcon />
        </IconButton>
        <DialogContent dividers>
          <Typography variant='h6'>Description:</Typography>
          <Typography gutterBottom>
            {desc}
          </Typography>
        </DialogContent>
        <DialogContent>
          <Typography variant='h6' dividers gutterBottom>Remote environment:</Typography>
            {remoteEnv.map(r=>{
                return(
                    <a>{r}</a>
                )
            })}
        </DialogContent>
        <DialogContent>
          <Typography variant='h6' dividers gutterBottom>
            Attachments:
          </Typography>
          {attachments.map(attach => {
              return(
                <><a href={attach}>{attach}</a><br></br></>
              )
            })}
        </DialogContent>

        <DialogActions>
        <TextField
          name="flag"
          fullWidth
          required
          value = {inputs.flag}
          onChange={handleChange}
          id="outlined-required"
          label="Flag"
          disabled = {solved ? true : false}
        />
          <Button variant='contained' color='secondary' autoFocus onClick={()=>{handleSubmit();handleClose()}} disabled = {solved ? true : false}>
            submit
          </Button>
        </DialogActions>
      </BootstrapDialog>

    </>
  )
}
