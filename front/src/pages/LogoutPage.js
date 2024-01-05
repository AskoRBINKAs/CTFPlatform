import { Box, Typography } from '@mui/material'
import React from 'react'
import { Navigate, useNavigate } from 'react-router-dom';

export default function LogoutPage() {
    localStorage.clear();
    const navigate = useNavigate();
    navigate("/")
  return (
    <div><Navigate to="/"></Navigate></div>
  )
}
