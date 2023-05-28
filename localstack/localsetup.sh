#!/bin/bash
awslocal secretsmanager create-secret --name ConnectionStrings__Mssql --secret-string 'test1234'
