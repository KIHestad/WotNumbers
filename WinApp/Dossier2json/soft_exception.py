# uncompyle6 version 3.7.0
# Python bytecode 2.7 (62211)
# Decompiled from: Python 2.7.18 (v2.7.18:8d21aa21f2, Apr 20 2020, 13:19:08) [MSC v.1500 32 bit (Intel)]
# Embedded file name: scripts/common/soft_exception.py


class SoftException(Exception):
    pass


class DisabledServiceSoftException(SoftException):

    def __init__(self, message='disabledService'):
        super(DisabledServiceSoftException, self).__init__(message)
        self.message = message