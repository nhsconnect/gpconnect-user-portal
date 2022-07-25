#!/bin/bash

psql --host database --username postgres --no-password < test_data_source.sql
