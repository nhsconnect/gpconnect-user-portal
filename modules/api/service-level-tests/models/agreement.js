module.exports = ({
    odsCode = "orgCode", 
    name = "test",
    addressLine1 = "test",
    addressLine2 = "test",
    town = "test",
    county = "test",
    country = "test",
    postcode = "test",
    htmlEnabled = false,
    structuredEnabled = false,
    sendDocumentEnabled = false,
    appointmentsEnabled = false,
    signatoryName = "test",
    signatoryEmail = "test",
    signatoryPosition = "test",
    softwareSupplier = "test",
    useCase = "test"
}) => ({
    organisation: {
        odsCode,
        name,
        addressLine1,
        addressLine2,
        town,
        county,
        country,
        postcode
    },
    interactions: {
        appointmentManagementEnabled: appointmentsEnabled,
        accessRecordHTMLEnabled: htmlEnabled,
        sendDocumentEnabled: sendDocumentEnabled,
        structuredRecordEnabled: structuredEnabled
    },
    signatory: {
        name: signatoryName,
        email: signatoryEmail,
        position: signatoryPosition
    },
    useCase,
    softwareSupplier
})