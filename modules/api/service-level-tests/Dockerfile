FROM node:18-alpine AS base

COPY package* .
RUN npm ci

COPY **/*.js .

ENTRYPOINT ["npm", "test"]