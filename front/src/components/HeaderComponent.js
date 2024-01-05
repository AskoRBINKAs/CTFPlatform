import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import EmojiFlagsIcon from '@mui/icons-material/EmojiFlags';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { IconButton } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import MenuItem from '@mui/material/MenuItem';
import Menu from '@mui/material/Menu';
export default function HeaderComponent(){
  var isLoggedIn = true;
  const navigate = useNavigate();
  const [anchorEl, setAnchorEl] = React.useState(null);


  const handleMenu = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <IconButton onClick={()=>navigate("/")}><EmojiFlagsIcon/></IconButton>
          <Typography variant="h6" component="div" sx={{ marginRight :'10px', marginLeft:'5px'}} >
            CosmoCTF
          </Typography>
          <Button color="inherit" onClick={()=>navigate("/tasks")}>Tasks</Button>
          <Button color="inherit" onClick={()=>navigate("/scoreboard")}>Scoreboard</Button>
          <Button color="inherit" onClick={()=>navigate("/rules")}>Rules</Button>
          {localStorage.getItem("admin")==="true" ? <Button color="inherit" onClick={()=>navigate("/admin")}>Admin Panel</Button> : null}
          <Typography sx={{flexGrow:1}}></Typography>
          {localStorage.getItem("username") ? <>{localStorage.getItem("username")}<IconButton onClick={handleMenu} color="inherit"><AccountCircle></AccountCircle></IconButton></> : <Button color="inherit" onClick={()=>navigate("/login")}>Login</Button> }
          {(<Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                open={Boolean(anchorEl)}
                onClose={handleClose}
              >
                <MenuItem onClick={handleClose}>Profile</MenuItem>
                <MenuItem onClick={()=>{navigate("/logout");handleClose()}}>Logout</MenuItem>
              </Menu>)}
        </Toolbar>
      </AppBar>
    </Box>
  )
}
